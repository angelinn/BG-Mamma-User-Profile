require_relative 'models'

require 'nokogiri'
require 'open-uri'

class BgMammaCrawler
    BASE_URL = 'http://www.bg-mamma.com'

    def initialize
        puts "Crawler initialized"
    end

    def crawl
        categories = get_categories
        topics = get_topics([categories.first])
        #topics.each { |t| get_comments(t) }
        puts get_comments(topics.first).first
    end

    def get_categories
        puts "Getting categories for #{BASE_URL}"

        page = Nokogiri::HTML(open(BASE_URL))
        page.css('li.uk-parent a').select { |n| n['href'].include?('board') }
    end

    def get_topics(categories)
        categories.map do |c|
            puts "Getting topics for #{c['href']}"
            category = Nokogiri::HTML(open("#{BASE_URL}#{c['href']}"))
            topics = []

            loop do
                page = category.css('li.uk-active').first            
                topics << category.css('p.topic-title a').map { |t| Topic.new(c['href'], t['href']) } 
                break unless page.next['uk-disabled'].nil?

                puts "Opening #{BASE_URL}#{page.next.css('a').first['href']}"
                category = Nokogiri::HTML(open("#{BASE_URL}#{page.next.css('a').first['href']}"))
                break
            end
            topics
        end.flatten
    end

    def get_comments(topic)
        topic_url = "#{BASE_URL}#{topic.url}"
        puts "Getting comments for #{topic_url}"

        Nokogiri::HTML(open(topic_url)).css('div.topic-post.tpl3').map do |post|
            user_name = post.css('p.user-name a').first
            user = User.new(user_name['href'], user_name.text.strip)
            content = post.css('div.post-content-inner').first.text.strip
            date = post.css('span.post-date').text

            Comment.new(user, content, topic, date)
        end
    end
end
