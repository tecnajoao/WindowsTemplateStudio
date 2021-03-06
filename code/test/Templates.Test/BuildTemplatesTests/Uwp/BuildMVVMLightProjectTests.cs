﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;

using Microsoft.TemplateEngine.Abstractions;
using Microsoft.Templates.Core;
using Microsoft.Templates.Core.Extensions;

using Xunit;

namespace Microsoft.Templates.Test.Build.Uwp
{
    [Collection("BuildTemplateTestCollection")]
    public class BuildMVVMLightProjectTests : BaseGenAndBuildTests
    {
        public BuildMVVMLightProjectTests(BuildTemplatesTestFixture fixture)
            : base(fixture, null, "MVVMLight")
        {
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMLight", "", Platforms.Uwp)]
        [Trait("ExecutionSet", "BuildMVVMLight")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildProjects")]
        public async Task Build_EmptyProject_InferConfig_Uwp(string projectType, string framework, string platform, string language)
        {
            var (projectName, projectPath) = await GenerateEmptyProjectAsync(projectType, framework, platform, language);

            // Don't delete after build test as used in inference test, which will then delete.
            AssertBuildProjectAsync(projectPath, projectName, platform, deleteAfterBuild: false);

            EnsureCanInferConfigInfo(projectType, framework, platform, projectPath);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMLight", "", Platforms.Uwp)]
        [Trait("ExecutionSet", "BuildMVVMLight")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildAllPagesAndFeatures")]
        [Trait("Type", "BuildRandomNames")]
        public async Task Build_All_ProjectNameValidation_G1_Uwp(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplatesGroup2.Contains(t.GroupIdentity)
                && !excludedTemplatesGroup2VB.Contains(t.GroupIdentity)
                && !t.GetIsHidden();

            var projectName = $"{ShortProjectType(projectType)}{CharactersThatMayCauseProjectNameIssues()}G1{ShortLanguageName(language)}";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetRandomName);

            AssertBuildProjectAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMLight", "", Platforms.Uwp)]
        [Trait("ExecutionSet", "BuildMVVMLight")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildAllPagesAndFeatures")]
        [Trait("Type", "BuildRandomNames")]
        public async Task Build_All_ProjectNameValidation_G2_Uwp(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !excludedTemplatesGroup1.Contains(t.GroupIdentity)
                && !excludedTemplatesGroup1VB.Contains(t.GroupIdentity)
                && !t.GetIsHidden();

            var projectName = $"{ShortProjectType(projectType)}{CharactersThatMayCauseProjectNameIssues()}G2{ShortLanguageName(language)}";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetRandomName);

            AssertBuildProjectAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMLight", ProgrammingLanguages.CSharp, Platforms.Uwp)]
        [Trait("ExecutionSet", "Minimum")]
        [Trait("ExecutionSet", "MinimumMVVMLight")]
        [Trait("ExecutionSet", "_CIBuild")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "CodeStyle")]
        public async Task BuildAndTest_All_CheckWithStyleCop_G2_Uwp(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !t.GetIsHidden()
                && !excludedTemplatesGroup1.Contains(t.GroupIdentity)
                || t.Identity == "wts.Feat.StyleCop";

            var projectName = $"{projectType}{framework}AllStyleCopG2";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName);

            AssertBuildProjectThenRunTestsAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMLight", ProgrammingLanguages.CSharp, Platforms.Uwp)]
        [Trait("ExecutionSet", "Minimum")]
        [Trait("ExecutionSet", "BuildMVVMLight")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "CodeStyle")]
        public async Task BuildAndTest_All_CheckWithStyleCop_G1_Uwp(string projectType, string framework, string platform, string language)
        {
            Func<ITemplateInfo, bool> templateSelector =
                t => t.GetTemplateType().IsItemTemplate()
                && (t.GetProjectTypeList().Contains(projectType) || t.GetProjectTypeList().Contains(All))
                && (t.GetFrontEndFrameworkList().Contains(framework) || t.GetFrontEndFrameworkList().Contains(All))
                && t.GetPlatform() == platform
                && !t.GetIsHidden()
                && !excludedTemplatesGroup2.Contains(t.GroupIdentity)
                || t.Identity == "wts.Feat.StyleCop";

            var projectName = $"{projectType}{framework}AllStyleCopG1";

            var projectPath = await AssertGenerateProjectAsync(projectName, projectType, framework, platform, language, templateSelector, BaseGenAndBuildFixture.GetDefaultName);

            AssertBuildProjectThenRunTestsAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetProjectTemplatesForBuild), "MVVMLight", "", Platforms.Uwp)]
        [Trait("ExecutionSet", "BuildMVVMLight")]
        [Trait("ExecutionSet", "_Full")]
        [Trait("Type", "BuildRightClick")]
        public async Task Build_Empty_AddRightClick_Uwp(string projectType, string framework, string platform, string language)
        {
            var projectName = $"{ShortProjectType(projectType)}AllRC{ShortLanguageName(language)}";

            var projectPath = await AssertGenerateRightClickAsync(projectName, projectType, framework, platform, language, true);

            AssertBuildProjectAsync(projectPath, projectName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetPageAndFeatureTemplatesForBuild), "MVVMLight", ProgrammingLanguages.CSharp, Platforms.Uwp, "")]
        [Trait("ExecutionSet", "BuildOneByOneMVVMLight")]
        [Trait("ExecutionSet", "_OneByOne")]
        [Trait("Type", "BuildOneByOneMVVMLight")]
        public async Task Build_MVVMLight_CS_OneByOneItems_Uwp(string itemName, string projectType, string framework, string platform, string itemId, string language)
        {
            var result = await AssertGenerationOneByOneAsync(itemName, projectType, framework, platform, itemId, language, false);

            AssertBuildProjectAsync(result.ProjectPath, result.ProjecName, platform);
        }

        [Theory]
        [MemberData(nameof(BaseGenAndBuildTests.GetPageAndFeatureTemplatesForBuild), "MVVMLight", ProgrammingLanguages.VisualBasic, Platforms.Uwp, "")]
        [Trait("ExecutionSet", "BuildOneByOneMVVMLight")]
        [Trait("ExecutionSet", "_OneByOne")]
        [Trait("Type", "BuildOneByOneMVVMLight")]
        public async Task Build_MVVMLight_VB_OneByOneItems_Uwp(string itemName, string projectType, string framework, string platform, string itemId, string language)
        {
            var result = await AssertGenerationOneByOneAsync(itemName, projectType, framework, platform, itemId, language, false);

            AssertBuildProjectAsync(result.ProjectPath, result.ProjecName, platform);
        }

    }
}
