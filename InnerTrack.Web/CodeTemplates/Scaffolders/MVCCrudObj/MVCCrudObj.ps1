[T4Scaffolding.Scaffolder(Description = "Creates the CRUD for an object 3 layers deep")][CmdletBinding()]
param(        
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true, Position = 1)][string]$ModelName,  
	[parameter(ValueFromPipelineByPropertyName = $true, Position = 2)][string]$baseNamespace,
    [parameter(ValueFromPipelineByPropertyName = $true, Position = 3)][string]$webNamespace,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$bllName = "BLL"
$dalName = "DAL"
$commonName = "Common"
if (!$baseNamespace) {
    Write-Host "d"
    $baseNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
    $lastPeriod = $baseNamespace.LastIndexOf(".Web")
    if ($lastPeriod -ge 0) { $baseNamespace = $baseNamespace.Substring(0, $lastPeriod) }
}
$firstPeriod = $baseNamespace.IndexOf(".")
$projectName = $baseNamespace
if ($firstPeriod -ge 0) { $projectName = $baseNamespace.Substring(0, $firstPeriod) }
$webNamespace = $WebNamespace
if (!$webNamespace) { $webNamespace = $baseNamespace + ".Web" }
$bllNamespace = $baseNamespace + "." + $bllName
$dalNamespace = $baseNamespace + "." + $dalName
$commonNamespace = $baseNamespace + "." + $commonName
 

#################
# BLL
#################
$output = "BLL\Logic\" + $ModelName + "Logic"
Add-ProjectItemViaTemplate $output -Template BLL\LogicTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Logic at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
    
#################
# BLLTest
#################
$output = "BLL.Test\Logic\" + $ModelName + "LogicTest"
Add-ProjectItemViaTemplate $output -Template BLLTest\LogicTestTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Logic Test at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

#################
# Common
#################
$output = "Common\Filters\" + $ModelName + "Filter"
Add-ProjectItemViaTemplate $output -Template Common\FilterTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Filter at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$output = "Common\Objs\" + $ModelName + "Obj"
Add-ProjectItemViaTemplate $output -Template Common\ObjTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Obj at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$output = "Common\Interfaces\Logic\I" + $ModelName + "Logic"
Add-ProjectItemViaTemplate $output -Template Common\ILogicTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added ILogic at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
   
$output = "Common\Interfaces\Database\I" + $projectName + "Repository_" + $ModelName
Add-ProjectItemViaTemplate $output -Template Common\IRepositoryMethodsTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added IRepository at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
    
#################
# DAL
#################
   
$output = "DAL\" + $projectName + "Repository_" + $ModelName
Add-ProjectItemViaTemplate $output -Template DAL\RepositoryMethodsTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Repository at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

#################
# Web
#################
$output = "Controllers\" + $ModelName + "Controller"
Add-ProjectItemViaTemplate $output -Template Controllers\ControllerTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Controller at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
    
$output = "Models\" + $ModelName + "Models"
Add-ProjectItemViaTemplate $output -Template Models\ModelsTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Models at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$class = Get-ProjectType ModelConverter
if(!$class) { 
    $output = "Helpers\ModelConverter"
    Add-ProjectItemViaTemplate $output -Template Helpers\ModelConverterClassTemplate `
	    -Model @{ 
            WebNamespace = $webNamespace; 
            BLLNamespace = $bllNamespace;
            DALNamespace = $dalNamespace; 
            CommonNamespace=$commonNamespace;
            ProjectName = $projectName;
            Model = $ModelName;
        } `
	    -SuccessMessage "Added ModelConverter at {0}" `
	    -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
        
    $class = Get-ProjectType ModelConverter
}
Add-ClassMemberViaTemplate -CodeClass $class -Template Helpers\ModelConverterMethodsTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Methods to ModelConverter at Helpers\ModelConverter.cs" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
    
$class = Get-ProjectType UrlHelperExtension

if(!$class) { 
    $output = "Helpers\UrlHelperExtension"
    Add-ProjectItemViaTemplate $output -Template Helpers\UrlHelperClassTemplate `
	    -Model @{ 
            WebNamespace = $webNamespace; 
            BLLNamespace = $bllNamespace;
            DALNamespace = $dalNamespace; 
            CommonNamespace=$commonNamespace;
            ProjectName = $projectName;
            Model = $ModelName;
        } `
	    -SuccessMessage "Added UrlHelperExtension at {0}" `
	    -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

    $class = Get-ProjectType UrlHelperExtension
}
Add-ClassMemberViaTemplate -CodeClass $class -Template Helpers\UrlHelperMethodsTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Methods to UrlHelper at Helpers\UrlHelperExtension.cs" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

    
$output = "Views\" + $ModelName + "\Edit"
Add-ProjectItemViaTemplate $output -Template Views\EditTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Edit View at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

$output = "Views\" + $ModelName + "\Index"
Add-ProjectItemViaTemplate $output -Template Views\IndexTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Index View at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
    
$output = "Views\" + $ModelName + "\Show"
Add-ProjectItemViaTemplate $output -Template Views\ShowTemplate `
	-Model @{ 
        WebNamespace = $webNamespace; 
        BLLNamespace = $bllNamespace;
        DALNamespace = $dalNamespace; 
        CommonNamespace=$commonNamespace;
        ProjectName = $projectName;
        Model = $ModelName;
    } `
	-SuccessMessage "Added Show View at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force