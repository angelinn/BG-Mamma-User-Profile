require 'oj'

class JsonSerializer
    class << self
    def serialize(object)
        File.write('stuff.json', (Oj::dump object, :indent => 2, :use_as_json => true))
    end
    end
end