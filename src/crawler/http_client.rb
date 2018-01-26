require 'nokogiri'
require 'open-uri'

class HttpClient
    def self.HTML(url)
        sleep 0.3
        Nokogiri::HTML(open(url))
    end
end