pipeline
{
	
	agent any

	triggers
	{
		pollSCM("H/5 * * * *")
	}

	stages
	{
		stage("CLEANUP")
		{
			steps
			{
				echo "CLEANUP STARTED"

				dir("Tests")
				{
					sh "rm -rf TestResults"
					sh "rm -rf screenshots"
				}

				echo "CLEANUP COMPLETED"
			}
		}
		stage("PREPARE")
		{
			steps
			{
				echo "PREPARE STARTED"
												
				dir("Tests")
				{
					sh "curl -qL https://www.npmjs.com/install.sh | sh"
					
					sh "npm install"
					sh "npm install junit"
					
					# Install testcafe and Video prerequisites
					sh "npm i -g testcafe"
					sh "npm install --save @ffmpeg-installer/ffmpeg"
					# Adds the testcafe reporter
					sh "npm i testcafe-reporter-jenkins"
					
					sh "export DISPLAY=:1"
					sh "(npm run start&)"
				}

				echo "PREPARE COMPLETED"
			}
		}
		stage("BUILD")
		{

			steps
			{
				echo "BUILD STARTED"
				
				sh "dotnet restore"
				sh "dotnet build AvengersWeb/AvengersWeb.csproj"
				
				echo "BUILD COMPLETED"
			}
		}
		stage("TEST")
		{
			steps
			{
				echo "TEST STARTED"
				
				dir("AvengersWeb")
				{
					sh "tmux new -s avengersweb -d 'dotnet run --release'"
					echo "STARTED TMUX SESSION"
				}

				dir("Tests")
				{
					sh "dotnet add package coverlet.collector"
					sh "dotnet test --collect:'XPlat Code Coverage'"
					sh "dotnet restore"
					sh "dotnet test Tests.csproj"
  					sh "export HTTP_ENV='http://localhost:5070' && testcafe chrome TestCafeTests.js -r jenkins:report.xml"
				}
				
				sh "tmux kill-ses -t avengersweb"
				echo "KILLED TMUX SESSION"
				
				echo "TEST COMPLETED"
			}
			post
			{
				success
				{
					archiveArtifacts "Tests/TestResults/*/coverage.cobertura.xml"
					publishCoverage adapters: [istanbulCoberturaAdapter(path: 'Tests/TestResults/*/coverage.cobertura.xml', thresholds:
					[[failUnhealthy: false, thresholdTarget: 'Conditional', unhealthyThreshold: 80.0, unstableThreshold: 50.0]])], checksName: '',
						sourceFileResolver: sourceFiles('NEVER_STORE')
					
					# Publish the report via junit
					junit keepLongStdio: true,
						testDataPublishers: [[$class: 'TestCafePublisher']],
						testResults: 'res.xml'
				}
			}
		}
		stage("DEPLOY")
		{
			steps
			{
			echo "DEPLOYMENT STARTED"
			
			echo "DEPLOYMENT COMPLETED"
			}
		}
	}
}
