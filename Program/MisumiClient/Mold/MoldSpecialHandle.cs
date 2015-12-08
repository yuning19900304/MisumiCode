using System;
using System.Collections.Generic;
using System.Text;
using Misumi_Client.Common;
using System.Windows.Forms;

namespace Misumi_Client.Mold
{
    class MoldSpecialHandle
    {
        ModelObject Model;
        public MoldSpecialHandle(ModelObject model)
        {
            Model = model;
        }
        public Form ReturnFormBySpecailType()
        {
            Form SpecialForm = null;

            //第一步，先进行空模型查找,如果表里有，返回值是false，则提示不能建模
            if (!Model.MoldService.CheckIsCanBuild(Model.E_CatalogType))
            {
                MoldLoad Mload = new MoldLoad(Model);
                Mload.MexEnd();
            }
            //第二步，进行特殊处理的操作
            switch (Model.E_CatalogType)
            {

                case "RCPNZ":
                case "RCPL":
                case "RCPN":
                case "RCPK":
                case "RFAL":
                case "RFAN":
                case "RCPLZ":
                    SpecialForm = new RCPNZ(Model);
                    break;
                case "TNPC":
                case "TNPF":
                case "TNPS":
                    SpecialForm = new TNP(Model);
                    break;
                case "MNT05":
                    SpecialForm = new MNT(Model);
                    break;
                case "MN10L":
                    SpecialForm = new MN10L(Model);
                    break;
                case "KMB":
                    SpecialForm = new KMB(Model);
                    break;
                case "KMS":
                    SpecialForm = new KMS(Model);
                    break;
                case "HIP-":
                case "HIPC-":
                case "HIPGT-":
                case "HIPH-":
                case "HIPL-":
                case "HIPP-":
                case "HIPX-":
                case "HIPXT-":
                    SpecialForm = new HIP(Model);
                    break;
                case "MN3":
                    Model.ModelType = "MN3BJ";
                    break;
                case "MN5":
                    Model.ModelType = "MN5BJ";
                    break;
            }

            if (SpecialForm != null)
                Model.IsSpecial = true;
            else
            {
                Model.ModelType = Model.E_CatalogType;
                SpecialForm = new MoldLoad(Model);
                Model.IsSpecial = false;
            }
            return SpecialForm;
        }
    }
}
