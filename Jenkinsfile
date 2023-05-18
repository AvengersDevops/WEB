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
				
				sh "docker build . -t avengersweb"
				
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

        			sh "docker-compose down"
        			sh "docker-compose up -d -e HTTP_ENV=http://0.0.0.0:81"

        			sh "docker-compose ps"

        			sh "docker-compose exec -T testcafe sh -c 'testcafe edge:headless /Tests/TestCafeTests.js -r junit:/reports/report.xml --skip-js-errors web'"

        			echo "DEPLOYMENT COMPLETED"
    			}
    			post 
			{
        			success 
				{
            				sh 'docker exec testcafe cp report.xml Tests/report.xml'

            				junit keepLongStdio: true,
                    			testResults: 'Tests/report.xml',
                    			skipPublishingChecks: true
            				archiveArtifacts "Tests/report.xml"
        			}
    			}
		}
	}
}
