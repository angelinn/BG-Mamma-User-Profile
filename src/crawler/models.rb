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

class Comment
    attr_accessor :user
    attr_accessor :content
    attr_accessor :topic

    def initialize(user, content, topic)
        self.user = user
        self.content = content
        self.topic = topic
    end
end