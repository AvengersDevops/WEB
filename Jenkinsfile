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
					sh "alias node='$PATH:/home/jenkins/.nvm/versions/node/v18.16.0/bin/node'"
					sh "alias npm='$PATH:/home/jenkins/.nvm/versions/node/v18.16.0/bin/npm'"
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
					sh "export HTTP_ENV=http://localhost:3000/"
					sh "./node_modules/.bin/testcafe chrome TestCafeTests.js -r xunit:res.xml"
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
					sh "junit 'res.xml'"
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
