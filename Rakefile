require 'bundler/setup'
require 'albacore'

desc 'Restore nuget packages for all projects'
nugets_restore :restore do |n|
	n.exe = 'tools/nuget/nuget.exe'
	n.out = 'packages'
end

desc 'Set the assembly version number'
asmver :version do |v|

	version_num = ENV['APPVEYOR_BUILD_VERSION'] ||= "1.0.0"

	v.file_path = "NuCache/Properties/AssemblyVersion.cs"
	v.attributes assembly_version: version_num,
				 assembly_file_version: version_num

end

desc 'Compile all projects'
build :compile do |msb|
	msb.target = [ :clean, :rebuild ]
	msb.sln = 'NuCache.sln'

	msb.add_parameter "/p:DeployOnBuild=true"
	msb.add_parameter "/p:PublishProfile=build"
end

desc 'Run all unit test assemblies'
test_runner :test do |xunit|
	xunit.exe = 'tools/xunit/xunit.console.clr4.exe'
	xunit.files = FileList['**/bin/*/*.tests.dll']
	xunit.add_parameter '/silent'
end

desc 'XCopy build output to a destination directory'
task :deploy, [ :destination ] do |t, args|

	destination = args.destination

	FileUtils.rm_rf(Dir.glob(File.join(destination, "*")))

	FileUtils.cp_r("build/.", destination)

end

task :default => [ :restore, :version, :compile, :test ]
