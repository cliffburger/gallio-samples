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

def test_file
  unix_path =File.join(root_path, 'MbUnit.Samples', 'bin', 'MbUnit.Samples.dll')
  unix_path.gsub('/', '\\' )
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


desc "Shows the location of gallio.echo"
task :show_gallio do
  puts mbunit_exe

  puts test_file
end

desc "Runs retry tests"
test_runner :mbunit_samples do |tests|
  tests.exe = mbunit_exe
  tests.files = test_file
  tests.add_parameter '/filter:Type:TheseTestsShouldRetry'
  tests.add_parameter '/rt:Html'
  tests.add_parameter '/show-reports'
  
end

desc "Example of category filtering"
test_runner :filter_samples do |tests|
  tests.exe = mbunit_exe
  tests.files = test_file
  # Order takes precedence
  tests.add_parameter '/filter:exclude Category:B,C include Category:Filtering'
  #tests.add_parameter '/filter:include Category:Filtering exclude Category:B,C'
  tests.add_parameter '/rt:Html'
  tests.add_parameter '/show-reports'
end

task :default => [:show_gallio]
