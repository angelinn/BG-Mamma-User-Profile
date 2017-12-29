class Category
    attr_accessor :topics
    attr_accessor :url

    def initialize(url)
        self.url = url
    end
end

class Topic
    attr_accessor :url
    attr_accessor :comments

    def initialize(url)
        self.url = url
    end

    def to_s
        "#{self.parent} - #{self.url}\n"
    end
end

class Comment
    attr_accessor :user
    attr_accessor :content
    attr_accessor :date
    attr_accessor :quotes

    def initialize(user, content, date, quotes)
        self.user = user
        self.content = content
        self.date = date
        self.quotes = quotes
    end
end

class Quote
    attr_accessor :quoter
    attr_accessor :content

    def initialize(quoter, content)
        self.quoter = quoter
        self.content = content
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