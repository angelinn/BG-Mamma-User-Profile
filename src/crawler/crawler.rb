require_relative 'models'
require_relative 'serializer'
require_relative 'http_client'

class BgMammaCrawler
    BASE_URL = 'http://www.bg-mamma.com'

    def initialize
        puts "Crawler initialized"
    end

    def crawl(start_topic=nil)
        categories = get_categories
        JsonSerializer.serialize(categories, 'topics', 'categories.json')

        categories.each do |c|
            for_each_topic(c) do |t| 
                unless start_topic.nil?
                    if start_topic != JsonSerializer.escape(t.name)
                        puts "Skipping #{t.name}..."
                    else
                        start_topic = nil
                    end
                    next
                end
                t.comments = get_comments(t)
                JsonSerializer.serialize(t, 'topics', "#{t.name}.json")

                # Free up memory
                t = nil
            end
        end
    end

    def get_categories
        puts "Getting categories for #{BASE_URL}"

        page = HttpClient::HTML(open(BASE_URL))
        page.css('li.uk-parent a').select { |n| n['href'].include?('board') }
                                  .map    { |c| Category.new(c['href'], c.text)}
    end

    def for_each_topic(c)
        puts "Getting topics for #{c.url}"
        category = HttpClient::HTML(open("#{BASE_URL}#{c.url}"))
        topics = []

        loop do
            page = category.css('li.uk-active').first 
            puts "Page #{page.text rescue 'last'}..."
            
            category_topics = category.css('p.topic-title a')
                                       .map { |t| Topic.new(t['href'], t.text, c.id) }
            if block_given?
                category_topics.each { |t| yield t }
            end

            topics << category_topics
            break if last_page? page

            puts "Opening #{BASE_URL}#{page.next.css('a').first['href']}"
            category = HttpClient::HTML(open(get_next_page_url(page)))
        end
        topics.flatten
    end

    def get_comments(topic)
        topic_url = "#{BASE_URL}#{topic.url}"
        puts "Getting comments for #{topic_url}"
        html = HttpClient::HTML(open(topic_url))
        comments = []
        
        loop do
            page = html.css('li.uk-active').first
            puts "Page #{page.text rescue 'last'}..."
        
            comments << get_single_page_comments(html)

            break if last_page? page
            html = HttpClient::HTML(open(get_next_page_url(page)))    
        end
        comments.flatten
    end

    def get_single_page_comments(topic_page)
        topic_page.css('div.topic-post.tpl3').map do |post|
            user_name = post.css('p.user-name a').first
            next if user_name.nil?
            user = User.new(user_name['href'], user_name.text.strip)
            content = post.css('div.post-content-inner').first.xpath('text()').text.strip
            date = post.css('span.post-date').text
            quotes = get_quotes(post)

            Comment.new(user, content, date, quotes)
        end
    end

    def last_page?(page_node)
        page_node.nil? || page_node.next['class'].include?('uk-disabled') rescue false
    end

    def get_next_page_url(page_node)
        "#{BASE_URL}#{page_node.next.css('a').first['href']}"
    end

    def get_quotes(comment_node)
        comment_node.css('div.quote-wrapper').map do |quote|
            quoter = quote.css('div.quoteheader.pb_exclude').text
            quoter = /Цитат на: (.*) в .*, .*, .*/.match(quoter)[1] rescue nil
            content = quote.css('div.quote.pb_exclude').first.text
            Quote.new(quoter, content)
        end                                                                                                                                              
    end
end
