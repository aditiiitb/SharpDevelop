// SharpDevelop samples
// Copyright (c) 2006, AlphaSierraPapa
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
//
// - Redistributions of source code must retain the above copyright notice, this list
//   of conditions and the following disclaimer.
//
// - Redistributions in binary form must reproduce the above copyright notice, this list
//   of conditions and the following disclaimer in the documentation and/or other materials
//   provided with the distribution.
//
// - Neither the name of the SharpDevelop team nor the names of its contributors may be used to
//   endorse or promote products derived from this software without specific prior written
//   permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS &AS IS& AND ANY EXPRESS
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
// IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using Mono.Build.Tasks;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace Mono.Build.Tasks.Tests
{
	[TestFixture]
	public class NormalMonoErrorParseTestFixture
	{
		Match match;
		
		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			string error = "MyClass.cs(19,7): warning CS0169: The private field `Foo.MyClass.foo' is never used";

			Regex regex = new Regex(MonoCSharpCompilerResultsParser.NormalErrorPattern, RegexOptions.Compiled);
			match = regex.Match(error);
		}
		
		[Test]
		public void Column()
		{
			Assert.AreEqual("7", match.Result("${column}"));
		}
		
		[Test]
		public void Line()
		{
			Assert.AreEqual("19", match.Result("${line}"));
		}
		
		[Test]
		public void FileName()
		{
			Assert.AreEqual("MyClass.cs", match.Result("${file}"));
		}
		
		[Test]
		public void Warning()
		{
			Assert.AreEqual("warning", match.Result("${error}"));
		}		
		
		[Test]
		public void ErrorNumber()
		{
			Assert.AreEqual("CS0169", match.Result("${number}"));
		}		
		
		[Test]
		public void ErrorText()
		{
			Assert.AreEqual("The private field `Foo.MyClass.foo' is never used", match.Result("${message}"));
		}				
	}
}
