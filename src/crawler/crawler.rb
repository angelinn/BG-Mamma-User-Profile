require 'nokogiri'
require 'open-uri'

class BgMammaCrawler
    BASE_URL = 'http://www.bg-mamma.com'

    def initialize
        puts "Crawler initialized"
    end

    def crawl
        get_categories.each { |n| puts n['href'] }
    end

    def get_categories
        page = Nokogiri::HTML(open(BASE_URL))
        page.css('li.uk-parent a').select { |n| n['href'].include?('board') }
    end
end
