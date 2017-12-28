class Topic
    attr_accessor :parent
    attr_accessor :url

    def initialize(parent, url)
        self.parent = parent
        self.url = url
    end

    def to_s
        "#{self.parent} - #{self.url}\n"
    end
end