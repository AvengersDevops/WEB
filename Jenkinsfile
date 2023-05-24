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
					sh "rm -rf TestResults/"
					sh "rm -rf screenshots/"
				}
				
				dir("PlaywrightTests")
				{
	                		sh "rm -rf TestResults/"
					sh "mkdir TestResults"
				}
				
				sh "chmod -R 777 PlaywrightTests/TestResults"
				
				echo "CLEANUP COMPLETED"
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
				sh "docker-compose up -d"
				
				dir("PlaywrightTests")
				{
					sh "pwsh bin/Debug/net7.0/playwright.ps1 install"
				    	sh "export URL=http://128.140.9.68:81 && dotnet test --test-adapter-path:. --logger:\"nunit;LogFilePath=TestResults/report.xml\""
				}

				echo "DEPLOYMENT COMPLETED"

    			}
    			post 
			{
        			success 
				{
            				archiveArtifacts "PlaywrightTests/TestResults/report.xml"
        			}
    			}
		}
	}
}
