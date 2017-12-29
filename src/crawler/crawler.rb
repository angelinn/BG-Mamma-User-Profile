require_relative 'models'
require_relative 'serializer'

require 'nokogiri'
require 'open-uri'

class BgMammaCrawler
    BASE_URL = 'http://www.bg-mamma.com'

    def initialize
        puts "Crawler initialized"
    end

    def crawl
        categories = get_categories
        puts "Got #{categories.count} categories."
        categories[0..2].each do |c|
            c.topics = get_topics(c)
            c.topics[0..2].each { |t| t.comments = get_comments(t); }
        end

        JsonSerializer.serialize categories
    end

    def get_categories
        puts "Getting categories for #{BASE_URL}"

        page = Nokogiri::HTML(open(BASE_URL))
        page.css('li.uk-parent a').select { |n| n['href'].include?('board') }
                                  .map    { |c| Category.new(c['href'])}
    end

    def get_topics(c)
        puts "Getting topics for #{c.url}"
        category = Nokogiri::HTML(open("#{BASE_URL}#{c.url}"))
        topics = []

        loop do
            page = category.css('li.uk-active').first            
            topics << category.css('p.topic-title a').map { |t| Topic.new(t['href']) } 
            break if last_page? page

            puts "Opening #{BASE_URL}#{page.next.css('a').first['href']}"
            category = Nokogiri::HTML(open(get_next_page_url(page)))
            break
        end
        topics.flatten
    end

    def get_comments(topic)
        topic_url = "#{BASE_URL}#{topic.url}"
        puts "Getting comments for #{topic_url}"
        html = Nokogiri::HTML(open(topic_url))
        comments = []
        c = 0
        loop do
            break if c == 3
            page = html.css('li.uk-active').first
            puts "Page #{page.text rescue 'last'}..."
            comments << get_single_page_comments(html)

            c += 1
            break if last_page? page
            html = Nokogiri::HTML(open(get_next_page_url(page)))            
        end
        comments.flatten
    end

    def get_single_page_comments(topic_page)
        topic_page.css('div.topic-post.tpl3').map do |post|
            user_name = post.css('p.user-name a').first
            user = User.new(user_name['href'], user_name.text.strip)
            content = post.css('div.post-content-inner').first.text.strip
            date = post.css('span.post-date').text
            quotes = get_quotes(post)

            Comment.new(user, content, date, quotes)
        end
    end

    def last_page?(page_node)
        page_node.nil? || (not page_node.next['uk-disabled'].nil?)
    end

    def get_next_page_url(page_node)
        "#{BASE_URL}#{page_node.next.css('a').first['href']}"
    end

    def get_quotes(comment_node)
        comment_node.css('div.quote-wrapper').map do |quote|
            quoter = quote.css('div.quoteheader.pb_exclude').text
            quoter = /Цитат на: (.*) в .*, .*, .*/.match(quoter)[1]
            content = quote.css('div.quote.pb_exclude').first.text
            Quote.new(quoter, content)
        end
    end
end
