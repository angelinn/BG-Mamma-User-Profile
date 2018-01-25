require 'nokogiri'
require 'open-uri'

class HttpClient
    def self.HTML(url)
        sleep 1
        Nokogiri::HTML(open(url))
    end
end