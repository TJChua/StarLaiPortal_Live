﻿using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using StarLaiPortal.Module.BusinessObjects.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarLaiPortal.Module.BusinessObjects.Warehouse_Transfer
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class WarehouseTransferReqDetailView : ObjectViewController<ListView, WarehouseTransferReqDetails>
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public WarehouseTransferReqDetailView()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            NewObjectViewController controller = Frame.GetController<NewObjectViewController>();
            if (controller != null)
            {
                //controller.NewObjectAction.Execute += NewObjectAction_Execute;
                controller.ObjectCreated += Controller_ObjectCreated;
            }
        }

        private void Controller_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            if (e.CreatedObject is WarehouseTransferReqDetails && View.IsRoot == false)
            {
                WarehouseTransferReqDetails currentObject = (WarehouseTransferReqDetails)e.CreatedObject;

                ListView lv = ((ListView)View);
                if (lv.CollectionSource is PropertyCollectionSource)
                {
                    PropertyCollectionSource collectionSource = (PropertyCollectionSource)lv.CollectionSource;
                    if (collectionSource.MasterObject != null)
                    {

                        if (collectionSource.MasterObjectType == typeof(WarehouseTransferReq))
                        {

                            WarehouseTransferReq masterobject = (WarehouseTransferReq)collectionSource.MasterObject;

                            if (masterobject.FromWarehouse != null)
                            {
                                currentObject.FromWarehouse = currentObject.Session.GetObjectByKey<vwWarehouse>(masterobject.FromWarehouse.WarehouseCode);
                            }

                            if (masterobject.ToWarehouse != null)
                            {
                                currentObject.ToWarehouse = currentObject.Session.GetObjectByKey<vwWarehouse>(masterobject.ToWarehouse.WarehouseCode);
                            }
                        }
                    }
                }

            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            NewObjectViewController controller = Frame.GetController<NewObjectViewController>();
            if (controller != null)
            {
                //controller.NewObjectAction.Execute -= NewObjectAction_Execute;
                controller.ObjectCreated -= Controller_ObjectCreated;
            }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
