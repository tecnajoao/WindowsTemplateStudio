﻿using ItemNamespace.Model;
using ItemNamespace.View;
namespace ItemNamespace.ViewModel
{
    public class PivotViewModel : ViewModelBase
    {
        public PivotViewModel()
        {
            //^^
            Items.Add(new PivotTabbedItem("PivotView_uct.ItemName".GetLocalized(), new uct.ItemNamePage()));
        }
    }
}