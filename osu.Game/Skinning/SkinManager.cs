﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Configuration;
using osu.Framework.Platform;
using osu.Game.Database;
using osu.Game.IO.Archives;

namespace osu.Game.Skinning
{
    public class SkinManager : ArchiveModelManager<SkinInfo, SkinFileInfo>
    {
        public Bindable<Skin> CurrentSkin = new Bindable<Skin>(new DefaultSkin());

        public override string[] HandledExtensions => new[] { ".osk" };

        /// <summary>
        /// Returns a list of all usable <see cref="SkinInfo"/>s.
        /// </summary>
        /// <returns>A list of available <see cref="SkinInfo"/>.</returns>
        public List<SkinInfo> GetAllUsableSkins() => ModelStore.ConsumableItems.Where(s => !s.DeletePending).ToList();

        protected override SkinInfo CreateModel(ArchiveReader archive) => new SkinInfo
        {
            Name = archive.Name
        };

        private SkinStore store;

        public SkinManager(Storage storage, DatabaseContextFactory contextFactory, IIpcHost importHost)
            : base(storage, contextFactory, new SkinStore(contextFactory, storage), importHost)
        {
        }
    }
}