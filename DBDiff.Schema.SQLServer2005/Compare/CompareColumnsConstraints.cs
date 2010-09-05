using System;
using System.Collections.Generic;
using System.Text;
using DBDiff.Schema.SQLServer.Model;

namespace DBDiff.Schema.SQLServer.Compare
{
    internal class CompareColumnsConstraints:CompareBase<ColumnConstraint>
    {
        public static ColumnConstraint GenerateDiferences(ColumnConstraint CamposOrigen, ColumnConstraint CamposDestino)
        {
            if ((CamposOrigen == null) && (CamposDestino != null))
            {
                CamposOrigen = CamposDestino;//.Clone((Column)CamposOrigen.Parent);
                CamposOrigen.Status = Enums.ObjectStatusType.CreateStatus;
                CamposOrigen.Parent.Status = Enums.ObjectStatusType.OriginalStatus;
                CamposOrigen.Parent.Parent.Status = Enums.ObjectStatusType.AlterStatus;                
            }
            if ((CamposOrigen != null) && (CamposDestino != null))
            {
                if (!ColumnConstraint.Compare(CamposOrigen, CamposDestino))
                {
                    CamposOrigen = CamposDestino.Clone((Column)CamposOrigen.Parent);
                    //Indico que hay un ALTER TABLE, pero sobre la columna, no seteo ningun estado.
                    CamposOrigen.Status = Enums.ObjectStatusType.AlterStatus;
                    CamposOrigen.Parent.Status = Enums.ObjectStatusType.OriginalStatus;
                    CamposOrigen.Parent.Parent.Status = Enums.ObjectStatusType.AlterStatus;
                }
            }
            if ((CamposOrigen != null) && (CamposDestino == null))
            {
                CamposOrigen.Status = Enums.ObjectStatusType.DropStatus;
                CamposOrigen.Parent.Status = Enums.ObjectStatusType.OriginalStatus;
                CamposOrigen.Parent.Parent.Status = Enums.ObjectStatusType.AlterStatus;
            }
            /*foreach (ColumnConstraint node in CamposDestino)
            {
                if (!CamposOrigen.Exists(node.FullName))
                {
                    node.Status = Enums.ObjectStatusType.CreateStatus;
                    CamposOrigen.Parent.Status = Enums.ObjectStatusType.OriginalStatus;
                    CamposOrigen.Parent.Parent.Status = Enums.ObjectStatusType.AlterStatus;
                    CamposOrigen.Add(node);
                }
                else
                {
                    if (!ColumnConstraint.Compare(CamposOrigen[node.FullName], node))
                    {
                        ColumnConstraint newNode = node.Clone(CamposOrigen.Parent);
                        //Indico que hay un ALTER TABLE, pero sobre la columna, no seteo ningun estado.
                        newNode.Status = Enums.ObjectStatusType.AlterStatus;
                        newNode.Parent.Status = Enums.ObjectStatusType.OriginalStatus;
                        newNode.Parent.Parent.Status = Enums.ObjectStatusType.AlterStatus;
                        CamposOrigen[node.FullName] = newNode;
                        
                    }
                }
            }

            MarkDrop(CamposOrigen, CamposDestino, node => 
            {
                node.Status = Enums.ObjectStatusType.DropStatus;
                CamposOrigen.Parent.Status = Enums.ObjectStatusType.OriginalStatus;
                CamposOrigen.Parent.Parent.Status = Enums.ObjectStatusType.AlterStatus;
            }
            );
            */
            return CamposOrigen;
        }
    }
}