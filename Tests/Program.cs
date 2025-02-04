﻿using NUnit.Framework;
using System.Linq;

namespace com.clusterrr.Famicom.NesTiler.Tests
{
    public class Tests
    {
        const string ImagesPath = "Images";
        const string ReferencesDir = "References";

        [Test]
        public void BelayaAkula()
        {
            var imagePath = Path.Combine(ImagesPath, "belaya_akula.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Buhanka()
        {
            var imagePath = Path.Combine(ImagesPath, "buhanka.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Chernobyl()
        {
            var imagePath = Path.Combine(ImagesPath, "chernobyl.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Dira()
        {
            var imagePath = Path.Combine(ImagesPath, "dira.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Glaza()
        {
            var imagePath = Path.Combine(ImagesPath, "glaza.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Gorgona()
        {
            var imagePath = Path.Combine(ImagesPath, "gorgona.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Myatejl()
        {
            var imagePath = Path.Combine(ImagesPath, "myatej.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Pagoda()
        {
            var imagePath = Path.Combine(ImagesPath, "pagoda.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Rayon4()
        {
            var imagePath = Path.Combine(ImagesPath, "rayon4.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Shkola()
        {
            var imagePath = Path.Combine(ImagesPath, "shkola.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Sindikat()
        {
            var imagePath = Path.Combine(ImagesPath, "sindikat.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Sputnik()
        {
            var imagePath = Path.Combine(ImagesPath, "sputnik.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Sworm()
        {
            var imagePath = Path.Combine(ImagesPath, "sworm.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void TrailerPark()
        {
            var imagePath = Path.Combine(ImagesPath, "trailer-park.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void WarfaceLogo()
        {
            var imagePath = Path.Combine(ImagesPath, "warface_logo.gif");
            DoTestSplit4(imagePath);
        }

        [Test]
        public void Jurassic()
        {
            var imagePath = Path.Combine(ImagesPath, "jurassic.png");
            DoTestSplit2(imagePath);
        }

        [Test]
        public void Jurassic2()
        {
            var imagePath = Path.Combine(ImagesPath, "jurassic2.png");
            DoTestSplit2(imagePath);
        }

        [Test]
        public void BlasterMasterLeft()
        {
            var imagePath = Path.Combine(ImagesPath, "blaster_master_left.png");
            DoTestNoSplit(imagePath);
        }

        [Test]
        public void BlasterMasterRight()
        {
            var imagePath = Path.Combine(ImagesPath, "blaster_master_right.png");
            DoTestNoSplit(imagePath);
        }

        [Test]
        public void BlasterMasterSharedPattern()
        {
            var imagePath1 = Path.Combine(ImagesPath, "blaster_master_left.png");
            var imagePath2 = Path.Combine(ImagesPath, "blaster_master_right.png");
            DoTestSharedPattern(imagePath1, imagePath2);
        }

        [Test]
        public void MeLossy()
        {
            var imagePath = Path.Combine(ImagesPath, "me.png");
            DoTestSplit2Lossy(imagePath);
        }

        [Test]
        public void Sprites8x8()
        {
            var imagePath = Path.Combine(ImagesPath, "sprites1.png");
            DoTestSprites8x8(imagePath);
        }

        [Test]
        public void Sprites8x16()
        {
            var imagePath = Path.Combine(ImagesPath, "sprites2.png");
            DoTestSprites8x16(imagePath);
        }

        private string PatternTablePath(string prefix, int number) => $"{prefix}_pattern_{number}.bin";
        private string NameTablePath(string prefix, int number) => $"{prefix}_name_table_{number}.bin";
        private string AttrTablePath(string prefix, int number) => $"{prefix}_attr_table_{number}.bin";
        private string PalettePath(string prefix, int number) => $"{prefix}_palette_{number}.bin";
        private string TilesCsvPath(string prefix) => $"{prefix}_tiles.csv";
        private string PalettesCsvPath(string prefix) => $"{prefix}_palettes.csv";
        private string SpritesCsvPath(string prefix) => $"{prefix}_sprites.csv";

        public void DoTestNoSplit(string imagePath)
        {
            var prefix = Path.GetFileNameWithoutExtension(imagePath);
            var args = new string[] {
                "--enable-palettes", "0,1,2,3",
                "--in-0", $"{imagePath}",
                "--out-pattern-table-0", PatternTablePath(prefix, 0),
                "--out-name-table-0", NameTablePath(prefix, 0),
                "--out-attribute-table-0", AttrTablePath(prefix, 0),
                "--out-palette-0", PalettePath(prefix, 0),
                "--out-palette-1", PalettePath(prefix, 1),
                "--out-palette-2", PalettePath(prefix, 2),
                "--out-palette-3", PalettePath(prefix, 3),
                "--out-tiles-csv", TilesCsvPath(prefix),
                "--out-palettes-csv", PalettesCsvPath(prefix),
                "--quiet",
            };
            var r = Program.Main(args);
            if (r != 0) throw new InvalidOperationException($"Return code: {r}");

            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 0)))), "pattern table");

            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 0)))), "nametable");

            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 0)))), "attribute table");

            Assert.That(File.ReadAllBytes(PalettePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 0)))), "palette 0");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 1)))), "palette 1");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 2)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 2)))), "palette 2");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 3)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 3)))), "palette 3");

            Assert.That(File.ReadAllLines(TilesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, TilesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "tiles CSV");
            Assert.That(File.ReadAllLines(PalettesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, PalettesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "palette CSV");
        }

        public void DoTestSharedPattern(string imagePath1, string imagePath2)
        {
            var prefix = Path.GetFileNameWithoutExtension(imagePath1) + "_" + Path.GetFileNameWithoutExtension(imagePath2);
            var args = new string[] {
                "--enable-palettes", "0,1,2,3",
                "--in-0", $"{imagePath1}",
                "--in-1", $"{imagePath2}",
                "--out-pattern-table", PatternTablePath(prefix, 0),
                "--out-name-table-0", NameTablePath(prefix, 0),
                "--out-name-table-1", NameTablePath(prefix, 1),
                "--out-attribute-table-0", AttrTablePath(prefix, 0),
                "--out-attribute-table-1", AttrTablePath(prefix, 1),
                "--out-palette-0", PalettePath(prefix, 0),
                "--out-palette-1", PalettePath(prefix, 1),
                "--out-palette-2", PalettePath(prefix, 2),
                "--out-palette-3", PalettePath(prefix, 3),
                "--share-pattern-table",
                "--out-tiles-csv", TilesCsvPath(prefix),
                "--out-palettes-csv", PalettesCsvPath(prefix),
                "--quiet",
            };
            var r = Program.Main(args);
            if (r != 0) throw new InvalidOperationException($"Return code: {r}");

            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 0)))), "pattern table");

            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 0)))), "nametable 0");
            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 1)))), "nametable 1");

            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 0)))), "attribute table 0");
            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 1)))), "attribute table 1");

            Assert.That(File.ReadAllBytes(PalettePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 0)))), "palette 0");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 1)))), "palette 1");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 2)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 2)))), "palette 2");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 3)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 3)))), "palette 3");

            Assert.That(File.ReadAllLines(TilesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, TilesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "tiles CSV");
            Assert.That(File.ReadAllLines(PalettesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, PalettesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "palette CSV");
        }

        public void DoTestSplit2(string imagePath)
        {
            var prefix = Path.GetFileNameWithoutExtension(imagePath);
            var args = new string[] {
                "--enable-palettes", "0,1,2,3",
                "--in-0", $"{imagePath}:0:128",
                "--in-1", $"{imagePath}:128:112",
                "--out-pattern-table-0", PatternTablePath(prefix, 0),
                "--out-pattern-table-1", PatternTablePath(prefix, 1),
                "--out-name-table-0", NameTablePath(prefix, 0),
                "--out-name-table-1", NameTablePath(prefix, 1),
                "--out-attribute-table-0", AttrTablePath(prefix, 0),
                "--out-attribute-table-1", AttrTablePath(prefix, 1),
                "--out-palette-0", PalettePath(prefix, 0),
                "--out-palette-1", PalettePath(prefix, 1),
                "--out-palette-2", PalettePath(prefix, 2),
                "--out-palette-3", PalettePath(prefix, 3),
                "--out-tiles-csv", TilesCsvPath(prefix),
                "--out-palettes-csv", PalettesCsvPath(prefix),
                "--quiet",
            };
            var r = Program.Main(args);
            if (r != 0) throw new InvalidOperationException($"Return code: {r}");

            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 0)))), "pattern table 0");
            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 1)))), "pattern table 1");

            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 0)))), "nametable 0");
            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 1)))), "nametable 1");

            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 0)))), "attribute table 0");
            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 1)))), "attribute table 1");

            Assert.That(File.ReadAllBytes(PalettePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 0)))), "palette 0");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 1)))), "palette 1");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 2)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 2)))), "palette 2");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 3)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 3)))), "palette 3");

            Assert.That(File.ReadAllLines(TilesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, TilesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "tiles CSV");
            Assert.That(File.ReadAllLines(PalettesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, PalettesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "palette CSV");
        }

        public void DoTestSplit2Lossy(string imagePath)
        {
            var prefix = Path.GetFileNameWithoutExtension(imagePath);
            var args = new string[] {
                "--enable-palettes", "0,1,2,3",
                "--in-0", $"{imagePath}:0:128",
                "--in-1", $"{imagePath}:128:112",
                "--out-pattern-table-0", PatternTablePath(prefix, 0),
                "--out-pattern-table-1", PatternTablePath(prefix, 1),
                "--out-name-table-0", NameTablePath(prefix, 0),
                "--out-name-table-1", NameTablePath(prefix, 1),
                "--out-attribute-table-0", AttrTablePath(prefix, 0),
                "--out-attribute-table-1", AttrTablePath(prefix, 1),
                "--out-palette-0", PalettePath(prefix, 0),
                "--out-palette-1", PalettePath(prefix, 1),
                "--out-palette-2", PalettePath(prefix, 2),
                "--out-palette-3", PalettePath(prefix, 3),
                "--lossy", "3",
                "--out-tiles-csv", TilesCsvPath(prefix),
                "--out-palettes-csv", PalettesCsvPath(prefix),
                "--quiet",
            };
            var r = Program.Main(args);
            if (r != 0) throw new InvalidOperationException($"Return code: {r}");

            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 0)))), "pattern table 0");
            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 1)))), "pattern table 1");

            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 0)))), "nametable 0");
            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 1)))), "nametable 1");

            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 0)))), "attribute table 0");
            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 1)))), "attribute table 1");

            Assert.That(File.ReadAllBytes(PalettePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 0)))), "palette 0");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 1)))), "palette 1");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 2)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 2)))), "palette 2");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 3)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 3)))), "palette 3");

            Assert.That(File.ReadAllLines(TilesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, TilesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "tiles CSV");
            Assert.That(File.ReadAllLines(PalettesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, PalettesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "palette CSV");
        }

        public void DoTestSplit4(string imagePath)
        {
            var prefix = Path.GetFileNameWithoutExtension(imagePath);
            var args = new string[] {
                "--enable-palettes", "0,1,2,3",
                "--in-0", $"{imagePath}:0:64",
                "--in-1", $"{imagePath}:64:64",
                "--in-2", $"{imagePath}:128:64",
                "--in-3", $"{imagePath}:192:48",
                "--out-pattern-table-0", PatternTablePath(prefix, 0),
                "--out-pattern-table-1", PatternTablePath(prefix, 1),
                "--out-pattern-table-2", PatternTablePath(prefix, 2),
                "--out-pattern-table-3", PatternTablePath(prefix, 3),
                "--out-name-table-0", NameTablePath(prefix, 0),
                "--out-name-table-1", NameTablePath(prefix, 1),
                "--out-name-table-2", NameTablePath(prefix, 2),
                "--out-name-table-3", NameTablePath(prefix, 3),
                "--out-attribute-table-0", AttrTablePath(prefix, 0),
                "--out-attribute-table-1", AttrTablePath(prefix, 1),
                "--out-attribute-table-2", AttrTablePath(prefix, 2),
                "--out-attribute-table-3", AttrTablePath(prefix, 3),
                "--out-palette-0", PalettePath(prefix, 0),
                "--out-palette-1", PalettePath(prefix, 1),
                "--out-palette-2", PalettePath(prefix, 2),
                "--out-palette-3", PalettePath(prefix, 3),
                "--out-tiles-csv", TilesCsvPath(prefix),
                "--out-palettes-csv", PalettesCsvPath(prefix),
                "--quiet",
            };
            var r = Program.Main(args);
            if (r != 0) throw new InvalidOperationException($"Return code: {r}");

            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 0)))), "pattern table 0");
            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 1)))), "pattern table 1");
            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 2)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 2)))), "pattern table 2");
            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 3)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 3)))), "pattern table 3");

            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 0)))), "nametable 0");
            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 1)))), "nametable 1");
            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 2)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 2)))), "nametable 2");
            Assert.That(File.ReadAllBytes(NameTablePath(prefix, 3)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, NameTablePath(prefix, 3)))), "nametable 3");

            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 0)))), "attribute table 0");
            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 1)))), "attribute table 1");
            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 2)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 2)))), "attribute table 2");
            Assert.That(File.ReadAllBytes(AttrTablePath(prefix, 3)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, AttrTablePath(prefix, 3)))), "attribute table 3");

            Assert.That(File.ReadAllBytes(PalettePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 0)))), "palette 0");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 1)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 1)))), "palette 1");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 2)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 2)))), "palette 2");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 3)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 3)))), "palette 3");

            Assert.That(File.ReadAllLines(TilesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, TilesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "tiles CSV");
            Assert.That(File.ReadAllLines(PalettesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, PalettesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "palette CSV");
        }

        public void DoTestSprites8x8(string imagePath)
        {
            var prefix = Path.GetFileNameWithoutExtension(imagePath);
            var args = new string[] {
                "--mode", "sprites8x8",
                "--enable-palettes", "0",
                "--in-0", imagePath,
                "--out-pattern-table-0", PatternTablePath(prefix, 0),
                "--out-palette-0", PalettePath(prefix, 0),
                "--out-tiles-csv", SpritesCsvPath(prefix),
                "--bg-color", "#000000",
                "--quiet",
            };
            var r = Program.Main(args);
            if (r != 0) throw new InvalidOperationException($"Return code: {r}");

            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 0)))), "pattern table 0");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 0)))), "palette 0");
            Assert.That(File.ReadAllLines(SpritesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, SpritesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "sprites CSV");
        }

        public void DoTestSprites8x16(string imagePath)
        {
            var prefix = Path.GetFileNameWithoutExtension(imagePath);
            var args = new string[] {
                "--mode", "sprites8x16",
                "--enable-palettes", "0",
                "--in-0", imagePath,
                "--out-pattern-table-0", PatternTablePath(prefix, 0),
                "--out-palette-0", PalettePath(prefix, 0),
                "--out-tiles-csv", SpritesCsvPath(prefix),
                "--bg-color", "#000000",
                "--quiet",
            };
            var r = Program.Main(args);
            if (r != 0) throw new InvalidOperationException($"Return code: {r}");

            Assert.That(File.ReadAllBytes(PatternTablePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PatternTablePath(prefix, 0)))), "pattern table 0");
            Assert.That(File.ReadAllBytes(PalettePath(prefix, 0)), Is.EqualTo(File.ReadAllBytes(Path.Combine(ReferencesDir, PalettePath(prefix, 0)))), "palette 0");
            Assert.That(File.ReadAllLines(SpritesCsvPath(prefix)).Select(l => l.Replace('/', '\\')), Is.EqualTo(File.ReadAllLines(Path.Combine(ReferencesDir, SpritesCsvPath(prefix))).Select(l => l.Replace('/', '\\'))), "sprites CSV");
        }
    }
}