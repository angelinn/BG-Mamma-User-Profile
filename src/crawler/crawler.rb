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
            break if page.nil? || (not page.next['uk-disabled'].nil?)

            puts "Opening #{BASE_URL}#{page.next.css('a').first['href']}"
            category = Nokogiri::HTML(open("#{BASE_URL}#{page.next.css('a').first['href']}"))
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
            c += 1          
            puts "Page #{page.text rescue 'last'}..."
            html.css('div.topic-post.tpl3').map do |post|
                user_name = post.css('p.user-name a').first
                user = User.new(user_name['href'], user_name.text.strip)
                content = post.css('div.post-content-inner').first.text.strip
                date = post.css('span.post-date').text
    
                comments << Comment.new(user, content, date)
            end

            break if page.nil? || (not page.next['uk-disabled'].nil?)
            html = Nokogiri::HTML(open("#{BASE_URL}#{page.next.css('a').first['href']}"))            
        end
        comments
    end
end
