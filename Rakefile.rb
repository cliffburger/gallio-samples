require 'pathname'
require 'fileutils'
require 'find'
require 'albacore'

def mbunit_exe
  find_in_packages "Gallio.Echo.exe"
end

def root_path
  Pathname.new(File.expand_path(File.dirname(__FILE__)))
end

def find_in_packages (file)
  package_path = File.join(root_path, 'packages')
  Find.find(package_path) do |f|
    return f if File.basename(f) == file
  end
end

def mbunit_commandline(assembly, mbunit_filter=nil)
  parameters = [assembly]
  parameters << "/rt:Html"
  parameters << "/rd:\"#{test_results}\""
  parameters << "/f:\"#{mbunit_filter}\"" if not_nil_or_empty(mbunit_filter)
  parameters
end

task :init_testrun do
  puts 'We really should build'
end

task :show_gallio do
  puts mbunit_exe
end

test_runner :mbunit_samples do |tests|
  tests.exe = mbunit_exe
  tests.files = 'C:\\cliff.src\\gallio-samples\\MbUnit.Samples\\bin\\MbUnit.Samples.dll'
  tests.add_parameter '/filter:Type:BasicTest'
end

task :default => [:show_gallio]
