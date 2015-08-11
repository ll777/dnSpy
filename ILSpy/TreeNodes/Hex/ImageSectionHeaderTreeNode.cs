﻿/*
    Copyright (C) 2014-2015 de4dot@gmail.com

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

using dnlib.PE;
using dnSpy.HexEditor;
using ICSharpCode.ILSpy.TreeNodes;

namespace dnSpy.TreeNodes.Hex {
	sealed class ImageSectionHeaderTreeNode : HexTreeNode {
		public override NodePathName NodePathName {
			get { return new NodePathName("secthdr", sectionNumber.ToString()); }
		}

		protected override string Name {
			get { return string.Format("Section #{0}: {1}", sectionNumber, imageSectionHeaderVM.NameVM.String); }
		}

		protected override object ViewObject {
			get { return imageSectionHeaderVM; }
		}

		readonly int sectionNumber;
		readonly ImageSectionHeaderVM imageSectionHeaderVM;

		public ImageSectionHeaderTreeNode(HexDocument doc, ImageSectionHeader sectHdr, int sectionNumber)
			: base((ulong)sectHdr.StartOffset, (ulong)sectHdr.EndOffset - 1) {
			this.sectionNumber = sectionNumber;
			this.imageSectionHeaderVM = new ImageSectionHeaderVM(doc, StartOffset);
		}

		protected override void OnDocumentModifiedOverride(ulong modifiedStart, ulong modifiedEnd) {
			imageSectionHeaderVM.OnDocumentModifiedOverride(modifiedStart, modifiedEnd);
			if (HexUtils.IsModified(imageSectionHeaderVM.NameVM.StartOffset, imageSectionHeaderVM.NameVM.EndOffset, modifiedStart, modifiedEnd))
				RaisePropertyChanged("Text");
		}
	}
}
