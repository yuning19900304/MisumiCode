using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Misumi_Client.Common;
using Misumi_Client.Press;

namespace Misumi_Client.PressSpecial
{
    class PressSpecialHandle
    {
        ModelObject Model;
        public PressSpecialHandle(ModelObject model)
        {
            Model = model;
        }
        public Form ReturnFormBySpecailType()
        {
            Form SpecialForm = null;
            try
            {
                //第一步，先进行空模型查找,如果表里有，返回值是false，则提示不能建模
                if (!Model.PressService.NoAcis(Model.E_CatalogType))
                {
                    CommonHelper.WriteAcisValue("NoMexType测试： PressSpecialHandle ReturnFormBySpecailType NoAcis");
                    PressLoad Pload = new PressLoad(Model);
                    Pload.MexEnd();
                }
                //第二步，进行特殊处理的操作
                string[] SpecialStr = Model.PressService.GetSpecialForm(Model.E_CatalogType).ToString().Split('|');
                if (SpecialStr.Length >= 3)
                {
                    if (SpecialStr[0] == "true")//如果是特殊处理
                    {
                        Model.IsSpecial = true;
                        switch (SpecialStr[2])//判断特殊处理的窗体名称
                        {
                            case "2_5-7B":
                                SpecialForm = new S2_5_7B(Model);
                                break;
                            case "2-10C":
                                SpecialForm = new S2_10C(Model);
                                break;
                            case "2-10D":
                                SpecialForm = new S2_10D(Model);
                                break;
                            case "2-11D":
                                SpecialForm = new S2_11D(Model);
                                break;
                            case "2-12H":
                                SpecialForm = new S2_12H(Model);
                                break;
                            case "2-18J":
                                SpecialForm = new S2_18J(Model);
                                break;
                            case "2-3C":
                                SpecialForm = new S2_3C(Model);
                                break;
                            case "2-4A":
                                SpecialForm = new S2_4A(Model);
                                break;
                            case "A2-4":
                                SpecialForm = new SA2_4(Model);
                                break;
                            case "2-4B":
                                SpecialForm = new S2_4B(Model);
                                break;
                            case "2-4C":
                                SpecialForm = new S2_4C(Model);
                                break;
                            case "2-4D":
                                SpecialForm = new S2_4D(Model);
                                break;
                            case "2-4E":
                                SpecialForm = new S2_4E(Model);
                                break;
                            case "27-28K":
                                SpecialForm = new S27_28K(Model);
                                break;
                            case "27-29K":
                                SpecialForm = new S27_29K(Model);
                                break;
                            case "2-5_8_9_11_12_14-18J":
                                SpecialForm = new S2_5_8_9_11_12_14_18J(Model);
                                break;
                            case "2-5B":
                                SpecialForm = new S2_5B(Model);
                                break;
                            case "2-5D":
                                SpecialForm = new S2_5D(Model);
                                break;
                            case "2-6A":
                                SpecialForm = new S2_6A(Model);
                                break;
                            case "2-6C":
                                SpecialForm = new S2_6C(Model);
                                break;
                            case "2-7B":
                                SpecialForm = new S2_7B(Model);
                                break;
                            case "2-7L":
                                SpecialForm = new S2_7L(Model);
                                break;
                            case "2-8B":
                                SpecialForm = new S2_8B(Model);
                                break;
                            case "2-8C":
                                SpecialForm = new S2_8C(Model);
                                break;
                            case "2-8L":
                                SpecialForm = new S2_8L(Model);
                                break;
                            case "3_4A":
                            case "3-4A":
                                SpecialForm = new S3_4A(Model);
                                break;
                            case "3-10D":
                                SpecialForm = new S3_10D(Model);
                                break;
                            case "3-11D":
                                SpecialForm = new S3_11D(Model);
                                break;
                            case "3-28K":
                                SpecialForm = new S3_28K(Model);
                                break;
                            case "3-28K-T":
                                SpecialForm = new S3_28K_T(Model);
                                break;
                            case "3-28K-C":
                                SpecialForm = new S3_28K_C(Model);
                                break;
                            case "3-28K-CP":
                                SpecialForm = new S3_28K_CP(Model);
                                break;
                            case "3-29K":
                                SpecialForm = new S3_29K(Model);
                                break;
                            case "3-4B":
                                SpecialForm = new S3_4B(Model);
                                break;
                            case "3-9D":
                                SpecialForm = new S3_9D(Model);
                                break;
                            case "5_7_8C":
                                SpecialForm = new S5_7_8C(Model);
                                break;
                            case "5_7-10C":
                                SpecialForm = new S5_7_10C(Model);
                                break;
                            case "6_7D":
                                SpecialForm = new S6_7D(Model);
                                break;
                            case "punchD":
                                SpecialForm = new SpunchD(Model);
                                break;
                            case "2-3-4E":
                                SpecialForm = new S2_3_4E(Model);
                                break;
                            case "RGBL":
                                SpecialForm = new SRGBL(Model);
                                break;
                            default:
                                LanguageManager LM = new LanguageManager("StartLoad");
                                CommonHelper.WriteAcisValue("NoMexType测试： PressSpecialHandle ReturnFormBySpecailType 进行特殊处理的操作");
                                MessageBox.Show(LM.SetLanguage("NoMexType"), LM.SetLanguage("strMsgTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                System.Environment.Exit(0);
                                break;
                        }
                    }
                    else
                    {
                        if ((SpecialStr[2]).Equals("TYPEINFO"))//如果返回TypeInfo，直接读TypeInfo表数据
                            Model.ModelType = Model.E_CatalogType;
                        else
                            Model.ModelType = SpecialStr[2];
                        SpecialForm = new PressLoad(Model);
                        Model.IsSpecial = false;
                    }
                }
                else
                {
                    Model.ModelType = Model.E_CatalogType;
                    SpecialForm = new PressLoad(Model);
                    Model.IsSpecial = false;
                }
            }
            catch (Exception ex)
            {
                Model.PressService.WriteApplicationErrorAsync(ex.Message, ex.Source, ex.StackTrace, Model.Region);
            }
            return SpecialForm;
        }
    }
}
