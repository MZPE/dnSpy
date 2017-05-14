﻿/*
    Copyright (C) 2014-2017 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace dnSpy.Contracts.Debugger.Evaluation.Engine {
	/// <summary>
	/// Creates <see cref="DbgEngineLanguage"/>s. Use <see cref="ExportDbgEngineLanguageProviderAttribute"/>
	/// to export an instance.
	/// </summary>
	public abstract class DbgEngineLanguageProvider {
		/// <summary>
		/// Gets the runtime display name, eg. ".NET Framework" or ".NET Core"
		/// </summary>
		public abstract string RuntimeDisplayName { get; }

		/// <summary>
		/// Creates all languages
		/// </summary>
		/// <returns></returns>
		public abstract IEnumerable<DbgEngineLanguage> Create();
	}

	/// <summary>Metadata</summary>
	public interface IDbgEngineLanguageProviderMetadata {
		/// <summary>See <see cref="ExportDbgEngineLanguageProviderAttribute.RuntimeGuid"/></summary>
		string RuntimeGuid { get; }
		/// <summary>See <see cref="ExportDbgEngineLanguageProviderAttribute.Order"/></summary>
		double Order { get; }
	}

	/// <summary>
	/// Exports a <see cref="DbgEngineLanguageProvider"/> instance
	/// </summary>
	[MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ExportDbgEngineLanguageProviderAttribute : ExportAttribute, IDbgEngineLanguageProviderMetadata {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="runtimeGuid">Runtime GUID, see <see cref="PredefinedDbgRuntimeGuids"/></param>
		/// <param name="order">Order</param>
		public ExportDbgEngineLanguageProviderAttribute(string runtimeGuid, double order = double.MaxValue)
			: base(typeof(DbgEngineLanguageProvider)) {
			RuntimeGuid = runtimeGuid ?? throw new ArgumentNullException(nameof(runtimeGuid));
			Order = order;
		}

		/// <summary>
		/// Runtime GUID, see <see cref="PredefinedDbgRuntimeGuids"/>
		/// </summary>
		public string RuntimeGuid { get; }

		/// <summary>
		/// Order
		/// </summary>
		public double Order { get; }
	}
}