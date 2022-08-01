using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using PMS.BL.Model;

namespace PMS.BL.Core
{
    public enum DocumentRoleType
    {
        Develops = 1,
        Agrees = 2,
        Claims =3
    }
    public enum StageStatusEnum
    {
        [Description("Ожидает выполнения")]
        Pending = 1,
        [Description("В процессе")]
        InProgress = 2,
        [Description("Выполнен")]
        Done = 3,
        [Description("Объединен")]
        Merged = 4,
        [Description("Отсутствует")]
        Missing = 5
    }

    public enum SectionEnum
    {
        [Description("НИР")]
        SRW = 1,
        [Description("ОКР")]
        EDW = 2,
    }

    public enum SRWStagesEnum
    {
        [Description("Выбор направлений исследований")]
        RD = 1,
        [Description("Теоретические и экспериментальные исследования")]
        TaER = 2,
        [Description("Проведение ОТР")]
        ETW = 3,
        [Description("Приемка НИР")]
        SRWA = 4
    }

    public enum EDWStagesEnum
    {
        [Description("Разработка ЭП")]
        PD = 1,
        [Description("Разработка ТП")]
        TP = 2,
        [Description("Разработка РКД для изготовления ОО изделия ВТ")]
        DWDD= 3,
        [Description("Изготовление ОО изделия ВТ и проведение ПИ")]
        MPaCPT= 4,
        [Description("Проведение ГИ ОО изделия ВТ")]
        CST = 5,
        [Description("Утверждение РКД для организации промышленного производства изделий ВТ")]
        AWDD = 6
    }

    public enum ProjectStatusEnum
    {
        [Description("Ожидает выполнения")]
        Pending = 1,
        [Description("В процессе")]
        InProgress = 2,
        [Description("Выполнен")]
        Finished = 3
    }

    public enum StageLineStatusEnum
    {
        [Description("Ожидает выполнения")]
        Pending = 1,
        [Description("В процессе")]
        InProgress = 2,
        [Description("Выполнен")]
        Finished = 3
    }

    public class PMSBLHelper
    {
        public static string GetDescriptionByEnumType(Enum ctype)
        {
            string description = (Attribute.GetCustomAttribute(ctype.GetType().GetField(ctype.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description;

            return description;
        }

        public static string EncryptPassword(string password)
        {
            string ret = "";

            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            ret = System.Text.Encoding.ASCII.GetString(data);

            return ret;
        }

        public static List<Stage> GetStagesBySection(Section section)
        {
            List<Stage> result = new List<Stage>();

            SectionEnum sectionEnum = (SectionEnum)section.SectionNum;

            switch (sectionEnum)
            {
                case SectionEnum.SRW:
                    foreach (var stageNum in Enum.GetValues(typeof(SRWStagesEnum)))
                    {
                        result.Add(new Stage(section.ProjectID, section.SectionNum, (int)stageNum, (int)StageStatusEnum.Pending));
                    }
                    break;
                case SectionEnum.EDW:
                    foreach (var stageNum in Enum.GetValues(typeof(EDWStagesEnum)))
                    {
                        result.Add(new Stage(section.ProjectID, section.SectionNum, (int)stageNum, (int)StageStatusEnum.Pending));
                    }
                    break;
                default:
                    throw new Exception("Wrong Section type");
            }
            return result;
        }
    }
}
