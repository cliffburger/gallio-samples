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

build :build do |b|
  b.sln = 'MbUnit.Samples.vs2010.sln'
  puts p
end

desc "Shows the location of gallio.echo"
task :show_gallio do
  puts mbunit_exe
  puts test_file
end

desc "Runs retry tests"
test_runner :retry => [:build] do |tests|
  mbunit_parameters tests, 'Type:TheseTestsShouldRetry'
end

desc "Example of category filtering -- exclude BC"
test_runner :f_a => [:build] do |tests|
  mbunit_parameters tests,'exclude Category:B,C include Category:Filtering'
end

desc "Example of category filtering -- attempt and fail to exclude B,C that also have 'Filtering'"
test_runner :filter_order_wrong => [:build] do |tests|
  mbunit_parameters tests,'include Category:Filtering exclude Category:B,C '
end

desc "Category filtering -- tests with category B (excluded) and category 'Filtering' are excluded"
test_runner :filter_order => [:build] do |tests|
  # first clause: tests with category B and Filtering are excluded,
  # second: include tests of category Filtering, other than those excluded
  # more specific types are always matched
  mbunit_parameters tests, 'exclude Category:B include Category:Filtering include Type:NoFilterCategory'
end

task :default => [:show_gallio]
