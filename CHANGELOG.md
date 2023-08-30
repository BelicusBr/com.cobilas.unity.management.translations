# Changelog
## [2.1.0] - 29/08/2023
## Changed
- As dependencias do pacote foram aluteradas.
## [2.0.0-ch1] - 28/08/2023
### Changed
- O autor do pacote foi alterado de `Cobilas CTB` para `BélicusBr`.
## [1.0.7] - 01/02/2023
### Added
- O objeto `ALFBTHeader` adicionado para substituir o objeto `ALFBTLanguage`.
### Changed
- O unity translations manager agora se utiliza o `LanguageManager`.
### Deprecated
A funções da `TranslationManager`
````c#
public static bool Load(ALFBTRead);
public static TextFlag GetTextFlag(string);
public static MarkingFlag GetMarkingFlag(string);
public static TranslationCollection GetTranslation(string);
````
### Removed
- Os objetos `ALFBTLanguage`, `ALFBTFlagBase` e `ALFBTMarkingFlag`
## [1.0.6] 15/01/2023
### Change
O `TranslationManager` foi modificado para se adequar.
#### Changed file
- ALFBTLanguage.cs
## [1.0.5] 17/11/2022
### Change 1
O `TranslationManager` está usando o novo `StartMethodOnRun` para inicialização.
### Change 2
Agora o `TranslationManager` vai chamar o método `void Refresh()` quando o </br>
projeto for construído ou quando o editor entra em modo `PlayModeStateChange.EnteredPlayMode`.
## [1.0.3] 13/08/2022
- Change Cobilas MG Translations.asset
- Change Runtime\ALFBTLanguage.cs
- Change Runtime\TranslationManager.cs
## [1.0.3] 13/08/2022
### Change
- Change Editor\Cobilas.Unity.Editor.Management.Translations.asmdef
- Change Runtime\Cobilas.Unity.Management.Translations.asmdef
- Change Runtime\TranslationManager.cs
### Add
- Add Runtime\TranslationList.cs
- Add Runtime\ALFBTTextFlag.cs
- Add Runtime\ALFBTMarkingFlag.cs
- Add Runtime\ALFBTLanguage.cs
- Add Runtime\ALFBTFlagBase.cs
- Add Editor\TranslationListInspector.cs
- Add Editor\ALFBTTextFlagInspector.cs
- Add Editor\ALFBTMarkingFlagInspector.cs
- Add Editor\ALFBTLanguageInspector.cs
### Remove
- Remove Editor\ALFBTObject.cs
- Remove Editor\ALFBTTextField.cs
## [1.0.3] 09/08/2022
- Change CHANGELOG.md
- Change Editor\ALFBTObject.cs
## [1.0.3] 08/08/2022
- Fix CHANGELOG.md
- Fix package.json
- Change Editor\ALFBTObject.cs
- Change Runtime\TranslationManager.cs
## [1.0.2] 31/07/2022
- Fix CHANGELOG.md
- Fix package.json
- Add Cobilas MG Translations.asset
- Remove Runtime\DependencyWarning.cs
- Remove Editor\DependencyWarning.cs
## [1.0.1] 22/07/2022
- Add CHANGELOG.md
- Add ALFBTObject.cs
- Add ALFBTTextField.cs
- Add Cobilas.Unity.Editor.Management.Translations.asmdef
- Add Editor/DependencyWarning.cs
- Fix Cobilas.Unity.Management.Translations.asmdef
- Fix Runtime/DependencyWarning.cs
- Fix TanslationManager.cs
- Fix package.json
## [1.0.0] 22/07/2022
- Add LICENSE.md
- Add Cobilas.Unity.Management.Translations.asmdef
- Add DependencyWarning.cs
- Add TanslationManager.cs
## [0.0.1] 22/07/2022
### Repositorio com.cobilas.unity.management.translations iniciado
- Lançado para o GitHub