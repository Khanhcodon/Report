using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : Address - public - Entity
    /// Access Modifiers: 
    /// Create Date : 091012
    /// Author      : GiangPN
    /// </author>
    /// <summary>
    /// <para>Định nghĩa địa chỉ lưu không gian user của 1 action(hướng chuyển)</para>
    /// (GiangPN@bkav.com - 091012)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Address
    {
        /// <summary>
        /// Lấy hoặc thiết lập ID của node chuyển tới
        /// </summary>
        /// <value>NodeFrom</value>
        [JsonProperty("NODE_FROM")]
        public int NodeFrom { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của node chuyển tới
        /// </summary>
        /// <value>NodeNameFrom</value>
        [JsonProperty("NODE_NAME_FROM")]
        public string NodeNameFrom { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập chuỗi giá trị lưu không gian user(những user có thể nhận được công văn) khi 1 node chuyển đến
        /// <para>Chuỗi giá trị đầy đủ nhất của AddStr: $pos:2.*|3$id:chinhtq@bkav.com$level:2|19$pos:2.29|3$rel:2@0|3.</para>
        /// <para>Mỗi tập giá trị(danh sách user) cách nhau bởi dấu $. Không gian user(tập hợp user) của node sẽ được định nghĩa bằng cách OR các tập giá trị ở trên.</para>
        /// <para>Tập hợp user theo vị trí: $pos:2.*|3</para>
        /// <para>Tập hợp user theo id: $userid:chinhtq@bkav.com - lấy user theo email.</para>
        /// <para>Tập hợp user theo cấp: $level:2|19 - lấy tất cả user cấp 2(theo cấp của cây phòng ban +1) có chức vụ id = 19.</para>
        /// <para>Tập hợp user theo quan hệ: $rel:2@0|3</para>
        /// </summary>
        /// <value>AddStr</value>
        [JsonProperty("ADD_STR")]
        public string AddStr { get; set; }

        //[JsonIgnore]
        //private List<QueryBase> _addStrQuery;

        private List<UserQuery> _userQueries;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("USER_QUERIES")]
        public List<UserQuery> UserQueries
        {
            get
            {
                if (_userQueries != null)
                {
                    return _userQueries;
                }
                if (string.IsNullOrEmpty(AddStr))
                {
                    return null;
                }
                _userQueries = new List<UserQuery>();
                var queries = AddStr.Split('$');
                if (queries.Length > 0)
                {
                    for (var i = 1; i < queries.Length; i++)
                    {
                        if (queries[i].Contains("userid"))
                        {
                            int userid;
                            if (int.TryParse(queries[i].Replace("userid:", ""), out userid))
                            {
                                _userQueries.Add(new UserQuery { UserId = userid });
                            }
                        }
                    }
                }
                return _userQueries;
            }
            set
            {
                _userQueries = value;
            }
        }

        private List<PositionQuery> _positionQueries;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("POSITION_QUERIES")]
        public List<PositionQuery> PositionQueries
        {
            get
            {
                if (_positionQueries != null)
                {
                    return _positionQueries;
                }
                if (string.IsNullOrEmpty(AddStr))
                {
                    return null;
                }
                _positionQueries = new List<PositionQuery>();
                var queries = AddStr.Split('$');
                if (queries.Length <= 0)
                {
                    return _positionQueries;
                }
                for (var i = 1; i < queries.Length; i++)
                {
                    if (queries[i].Contains("userid"))
                    {
                        continue;
                    }
                    if (queries[i].ToCharArray().Where(t => t == '|').Count() != 1)
                    {
                        continue;
                    }
                    var query = queries[i].Split('|')[0];
                    var position = queries[i].Split('|')[1];
                    if (!query.Contains("pos"))
                    {
                        continue;
                    }
                    PositionType posType;
                    var deptIdExt = query.Replace("pos:", "");
                    if (query.Contains(".*"))
                    {
                        posType = PositionType.DonViTrucThuoc;
                        deptIdExt = deptIdExt.Replace(".*", "");
                    }
                    else if (query.Contains(".?"))
                    {
                        posType = PositionType.DonViCapDuoi;
                        deptIdExt = deptIdExt.Replace(".?", "");
                    }
                    else
                    {
                        posType = PositionType.DonViHienTai;
                    }
                    int positionid;
                    if (int.TryParse(position, out positionid))
                    {
                        _positionQueries.Add(new PositionQuery
                        {
                            PositionId = positionid,
                            Position = posType,
                            DepId = WorkflowExtension.GetDepIdFromExt(deptIdExt)
                        });
                    }
                }
                return _positionQueries;
            }
            set
            {
                _positionQueries = value;
            }
        }

        private List<LevelQuery> _levelQueries;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("LEVEL_QUERIES")]
        public List<LevelQuery> LevelQueries
        {
            get
            {
                if (_levelQueries != null)
                {
                    return _levelQueries;
                }
                if (string.IsNullOrEmpty(AddStr))
                {
                    return null;
                }
                _levelQueries = new List<LevelQuery>();
                var queries = AddStr.Split('$');
                if (queries.Length <= 0)
                {
                    return _levelQueries;
                }
                for (var i = 1; i < queries.Length; i++)
                {
                    if (queries[i].Contains("userid"))
                    {
                        continue;
                    }
                    if (queries[i].ToCharArray().Where(t => t == '|').Count() != 1)
                    {
                        continue;
                    }
                    var query = queries[i].Split('|')[0];
                    var position = queries[i].Split('|')[1];
                    if (query.Contains("level"))
                    {
                        int levelId, positionid;
                        if (int.TryParse(query.Replace("level:", ""), out levelId) && int.TryParse(position, out positionid))
                        {
                            _levelQueries.Add(new LevelQuery { PositionId = positionid, Level = levelId });
                        }
                    }
                }
                return _levelQueries;
            }
            set
            {
                _levelQueries = value;
            }
        }

        private List<RelationQuery> _relationQueries;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("RELATION_QUERIES")]
        public List<RelationQuery> RelationQueries
        {
            get
            {
                if (_relationQueries != null)
                {
                    return _relationQueries;
                }
                if (string.IsNullOrEmpty(AddStr))
                {
                    return null;
                }
                _relationQueries = new List<RelationQuery>();
                var queries = AddStr.Split('$');
                if (queries.Length <= 0)
                {
                    return _relationQueries;
                }
                for (var i = 1; i < queries.Length; i++)
                {
                    if (queries[i].Contains("userid"))
                    {
                        continue;
                    }
                    if (queries[i].ToCharArray().Where(t => t == '|').Count() != 1)
                    {
                        continue;
                    }
                    var query = queries[i].Split('|')[0];
                    var position = queries[i].Split('|')[1];
                    if (!query.Contains("rel"))
                    {
                        continue;
                    }
                    RelationType relType;
                    string strRelTypeTemp;
                    if (query.Contains("@0"))
                    {
                        relType = RelationType.CanBoCungDonVi;
                        strRelTypeTemp = "@0";
                    }
                    else if (query.Contains("@~"))
                    {
                        relType = RelationType.CanBoCungCap;
                        strRelTypeTemp = "@~";
                    }
                    else if (query.Contains("@^"))
                    {
                        relType = RelationType.CanBoCungNutCha;
                        strRelTypeTemp = "@^";
                    }
                    else if (query.Contains("@1"))
                    {
                        relType = RelationType.CanBoCapDuoi;
                        strRelTypeTemp = "@1";
                    }
                    else
                    {
                        relType = RelationType.CanBoCapTren;
                        strRelTypeTemp = "@-1";
                    }
                    int positionId;
                    if (int.TryParse(position, out positionId))
                    {
                        _relationQueries.Add(new RelationQuery
                        {
                            PositionId = positionId,
                            Relation = relType,
                            NodeId = WorkflowExtension.GetDepIdFromExt(
                                query.Replace("rel:", "").Replace(
                                    strRelTypeTemp, ""))
                        });
                    }
                }
                return _relationQueries;
            }
            set
            {
                _relationQueries = value;
            }
        }

        /// <summary>
        /// Query lấy không gian user. Parse từ AddStr hoặc ADD_STR;
        /// </summary>
        [JsonIgnore]
        public List<QueryBase> Queries
        {
            get
            {
                //Không xóa đi phần comment này vì có thể sẽ dùng để convert từ dữ liệu eOffice sang
                //if (string.IsNullOrEmpty(AddStr))
                //{
                //    return new List<QueryBase>();
                //}
                //if (_addStrQuery != null)
                //{
                //    return _addStrQuery;
                //}

                //// Xử lý phân tích chuỗi ADD_STR thành QueryBase
                //_addStrQuery = new List<QueryBase>();

                //var userQueries = new List<UserQuery>();

                //var positionQueries = new List<PositionQuery>();

                //var levelQueries = new List<LevelQuery>();

                //var relationQueries = new List<RelationQuery>();

                //var queries = AddStr.Split('$');
                //if (queries.Length > 0)
                //{
                //    for (var i = 1; i < queries.Length; i++)
                //    {
                //        if (queries[i].Contains("userid"))
                //        {
                //            userQueries.Add(new UserQuery { UserId = int.Parse(queries[i].Replace("userid:", "")) });
                //        }
                //        else
                //        {
                //            if (queries[i].ToCharArray().Where(t => t == '|').Count() == 1)
                //            {
                //                var query = queries[i].Split('|')[0];
                //                var jobtitle = queries[i].Split('|')[1];
                //                if (query.Contains("level"))
                //                {
                //                    levelQueries.Add(new LevelQuery { Jobtitle = int.Parse(jobtitle), Level = int.Parse(query.Replace("level:", "")) });
                //                }
                //                else if (query.Contains("pos"))
                //                {
                //                    PositionType posType;
                //                    var deptIdExt = query.Replace("pos:", "");
                //                    if (query.Contains(".*"))
                //                    {
                //                        posType = PositionType.DonViTrucThuoc;
                //                        deptIdExt = deptIdExt.Replace(".*", "");
                //                    }
                //                    else if (query.Contains(".?"))
                //                    {
                //                        posType = PositionType.DonViCapDuoi;
                //                        deptIdExt = deptIdExt.Replace(".?", "");
                //                    }
                //                    else
                //                    {
                //                        posType = PositionType.DonViHienTai;
                //                    }
                //                    positionQueries.Add(new PositionQuery
                //                                            {
                //                                                Jobtitle = int.Parse(jobtitle),
                //                                                Position = posType,
                //                                                DepId =
                //                                                    WorkflowExtension.GetDepIdFromExt(deptIdExt)
                //                                            });
                //                }
                //                else if (query.Contains("rel"))
                //                {
                //                    RelationType relType;
                //                    string strRelTypeTemp;
                //                    if (query.Contains("@0"))
                //                    {
                //                        relType = RelationType.CanBoCungDonVi;
                //                        strRelTypeTemp = "@0";
                //                    }
                //                    else if (query.Contains("@~"))
                //                    {
                //                        relType = RelationType.CanBoCungCap;
                //                        strRelTypeTemp = "@~";
                //                    }
                //                    else if (query.Contains("@^"))
                //                    {
                //                        relType = RelationType.CanBoCungNutCha;
                //                        strRelTypeTemp = "@^";
                //                    }
                //                    else if (query.Contains("@1"))
                //                    {
                //                        relType = RelationType.CanBoCapDuoi;
                //                        strRelTypeTemp = "@1";
                //                    }
                //                    else
                //                    {
                //                        relType = RelationType.CanBoCapTren;
                //                        strRelTypeTemp = "@-1";
                //                    }
                //                    relationQueries.Add(new RelationQuery
                //                    {
                //                        Jobtitle = int.Parse(jobtitle),
                //                        Relation = relType,
                //                        DepId =
                //                            WorkflowExtension.GetDepIdFromExt(
                //                                query.Replace("rel:", "").Replace(
                //                                    strRelTypeTemp, ""))
                //                    });
                //                }
                //            }

                //        }
                //    }
                //}

                //_addStrQuery.AddRange(userQueries);

                //_addStrQuery.AddRange(positionQueries);

                //_addStrQuery.AddRange(levelQueries);

                //_addStrQuery.AddRange(relationQueries);

                //return _addStrQuery;

                var queries = new List<QueryBase>();
                if (UserQueries != null)
                {
                    queries.AddRange(UserQueries);
                }
                if (PositionQueries != null)
                {
                    queries.AddRange(PositionQueries);
                }
                if (LevelQueries != null)
                {
                    queries.AddRange(LevelQueries);
                }
                if (RelationQueries != null)
                {
                    queries.AddRange(RelationQueries);
                }
                return queries;
            }
        }
    }
}