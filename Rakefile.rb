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

def mbunit_parameters(test_task, mbunit_filter)
  test_task.exe = mbunit_exe
  test_task.files = test_file
  test_task.add_parameter "/rt:Html"
  test_task.add_parameter "/show-reports"
  #test_task.add_parameter "/rd:\"#{test_results}\""
  test_task.add_parameter"/f:#{mbunit_filter}"
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
  mbunit_parameters tests, 'Type:TheseTestsShouldRetry'
end

desc "Example of category filtering -- exclude BC"
test_runner :filter_a1 do |tests|
  mbunit_parameters tests,'exclude Category:B,C include Category:Filtering'
end

desc "Example of category filtering -- attempt to exclude BC"
test_runner :filter_a2 do |tests|
  mbunit_parameters tests,'include Category:Filtering exclude Category:B,C '
end

desc "Example of category filtering -- sequence without and"
test_runner :filter_combine do |tests|
  mbunit_parameters tests, 'exclude Category:B include Category:Filtering include Type:NoFilterCategory'
end

task :default => [:show_gallio]
