require 'oj'

class JsonSerializer
    class << self    
    def serialize(object, folder_name, file_name)
        file_name = escape(file_name)
        puts file_name
        File.write("#{folder_name}/#{file_name}", (Oj::dump object, :indent => 2, :use_as_json => true))
    end

    def escape(file_name)
        file_name.gsub(/[\x00\\:\*\?\"\/<>\|]/, '_')       
    end
    end
end
