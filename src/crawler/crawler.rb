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
        page = Nokogiri::HTML(open(BASE_URL))
        page.css('li.uk-parent a').select { |n| n['href'].include?('board') }
    end

    def get_topics(categories)
        categories.map do |c|
            category = Nokogiri::HTML(open("#{BASE_URL}#{c['href']}"))
            category.css('p.topic-title a').map { |t| Topic.new(c['href'], t['href']) } 
        end
    end
end
