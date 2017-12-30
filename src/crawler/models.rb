class Category
    attr_accessor :name
    attr_accessor :url
    attr_accessor :id

    @@id = 1

    def initialize(url, name)
        self.url = url
        self.name = name
        self.id = @@id

        @@id += 1        
    end
end

class Topic
    attr_accessor :url
    attr_accessor :comments
    attr_accessor :category_id
    attr_accessor :name

    def initialize(url, name, category_id)
        self.url = url
        self.name = name
        self.category_id = category_id
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