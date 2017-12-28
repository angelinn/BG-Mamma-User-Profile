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
        get_topics([categories.first]).each { |n| puts n }
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
            end
            topics
        end
    end
end
