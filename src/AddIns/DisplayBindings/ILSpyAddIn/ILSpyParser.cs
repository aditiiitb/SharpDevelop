﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using ICSharpCode.Core;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.SharpDevelop.Editor.Search;
using ICSharpCode.SharpDevelop.Parser;
using ICSharpCode.SharpDevelop.Project;

namespace ICSharpCode.ILSpyAddIn
{
	/// <summary>
	/// This class "parses" a decompiled type to provide the information required
	/// by the ParserService.
	/// </summary>
	public class ILSpyParser : IParser
	{
		public bool CanParse(string fileName)
		{
			return fileName != null && fileName.StartsWith("ilspy://", StringComparison.OrdinalIgnoreCase);
		}
		
		public ParseInformation Parse(FileName fileName, ITextSource fileContent, bool fullParseInformationRequested, IProject parentProject, CancellationToken cancellationToken)
		{
			return ILSpyDecompilerService.ParseDecompiledType(DecompiledTypeReference.FromFileName(fileName), cancellationToken);
		}
		
		public ResolveResult Resolve(ParseInformation parseInfo, TextLocation location, ICompilation compilation, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		
		public ResolveResult ResolveSnippet(ParseInformation parseInfo, TextLocation location, string codeSnippet, ICompilation compilation, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		
		public void FindLocalReferences(ParseInformation parseInfo, ITextSource fileContent, IVariable variable, ICompilation compilation, Action<SearchResultMatch> callback, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		
		static readonly Lazy<IAssemblyReference[]> defaultReferences = new Lazy<IAssemblyReference[]>(
			delegate {
				Assembly[] assemblies = {
					typeof(object).Assembly,
					typeof(Uri).Assembly,
					typeof(Enumerable).Assembly
				};
				return assemblies.Select(asm => new CecilLoader().LoadAssemblyFile(asm.Location)).ToArray();
			});
		
		public ICompilation CreateCompilationForSingleFile(FileName fileName, IUnresolvedFile unresolvedFile)
		{
			return new CSharpProjectContent()
				.AddAssemblyReferences(defaultReferences.Value)
				.AddOrUpdateFiles(unresolvedFile)
				.CreateCompilation();
		}
		
		public IReadOnlyList<string> TaskListTokens { get; set; }
	}
}
