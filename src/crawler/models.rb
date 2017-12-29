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
    attr_accessor :date

    def initialize(user, content, topic, date)
        self.user = user
        self.content = content
        self.topic = topic
        self.date = date
    end
end
    
class User
    attr_accessor :profile_url
    attr_accessor :user_name

    def initialize(url, name)
        self.profile_url = url
        self.user_name = name
    end

    def to_s
        "#{self.user_name} - #{self.profile_url}\n"
    end
end