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
					sh "npm install"
					
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

				dir("Tests")
				{
					sh "dotnet add package coverlet.collector"
					sh "dotnet test --collect:'XPlat Code Coverage'"
					sh "dotnet restore"
					sh "dotnet test Tests.csproj"
				}
				
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
				}
			}
		}
		stage("DEPLOY")
		{
			steps
			{
				echo "DEPLOYMENT STARTED"
			
				dir("AvengersWeb")
				{
					sh "tmux new -s avengersweb -d 'dotnet run --release'"
					echo "STARTED TMUX SESSION"
				}
				
				dir("Tests")
				{
  					sh "export HTTP_ENV='http://localhost:5070' && ./node_modules/.bin/testcafe chrome:headless TestCafeTests.js -r xunit:report.xml"
				}
				
				sh "tmux kill-ses -t avengersweb"
				echo "KILLED TMUX SESSION"
				
				echo "DEPLOYMENT COMPLETED"
			}
			post
			{
				success
				{	
					junit keepLongStdio: true,
						testResults: 'Tests/report.xml',
						skipPublishingChecks: true
					archiveArtifacts "Tests/report.xml"
				}
			}
		}
	}
}
