using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDemonstrator.ADSInterface;

namespace SystemDemonstrator
{
    class c_CellError : c_Base_ADSReadInterface
    {
        c_ErrorHandler m_cErrorHAndler;

        public String sCellEror { get; set; }
        public c_CellError (c_ErrorHandler cErrorHAndler)
        {

            c_ErrorHandler m_cErrorHAndler;

            ADSReadJSon = new ADSReadInterface("MAIN.sJsonDocError");
            ADSReadJSon.Registrate((c_Base_ADSReadInterface)this);

        }

        public override void Update(string sJSon)
        {

            sCellEror = sJSon;
            m_cErrorHAndler.Update();


        }
    }


}
