require 'oj'

class JsonSerializer
    class << self
    def serialize(object, file_name)
        file_name.gsub!(/[\x00\\:\*\?\"<>\|]/, '_')       
        File.write(file_name, (Oj::dump object, :indent => 2, :use_as_json => true))
    end
    end
end