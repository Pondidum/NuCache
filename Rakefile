require 'bundler/setup'
require 'albacore'

nugets_restore :restore do |n|
	n.exe = 'tools/nuget/nuget.exe'
	n.out = 'packages'
end

build :compile do |msb|
	msb.target = [ :clean, :rebuild ]
	msb.sln = 'NuCache.sln'

	msb.add_parameter "/p:DeployOnBuild=true"
	msb.add_parameter "/p:PublishProfile=build"
end

test_runner :test do |xunit|
	xunit.exe = 'tools/xunit/xunit.console.clr4.exe'
	xunit.files = FileList['**/bin/*/*.tests.dll']
	xunit.add_parameter '/silent'
end

task :deploy, [ :destination ] do |t, args|

	destination = args.destination

	FileUtils.rm_rf(Dir.glob(File.join(destination, "*")))

	FileUtils.cp_r("build/.", destination)

end

task :default => [ :restore, :compile, :test ]
