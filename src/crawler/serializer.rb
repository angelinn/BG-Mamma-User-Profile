require 'oj'

class JsonSerializer
    class << self
    def serialize(object, file_name)
        File.write(file_name, (Oj::dump object, :indent => 2, :use_as_json => true))
    end
    end
end