
/// <summary> Các mẫu </summary>
(function (eR) {
    eR.template = {
        dataFieldPanel: '\
		<div class="data-fields"> \
            <ul> \
                <li class="jstree-open"> \
                    <span class="toolbox-field-header">\
                        <span>▼ Datas</span>\
                        <input type="button" title="Thêm trường dữ liệu" value="+" class="add-data-field"/>\
                    </span> \
                    <ul> \
		                {{each(i, datafield) dataFields}} \
                            <li> \
                            <div class="report-toolbox-field" value="${datafield.value}" title="${datafield.value}" datatype="${datafield.datatype}">${datafield.name}</div>\
                            <input type="button" value="x" title="Xóa trường dữ liệu này" class="delete-field"/>\
                            </li> \
		                {{/each}}\
                    </ul>\
                </li>\
            </ul>\
        </div>',

        commonFieldPanel: '\
            <div class="special-fields"> \
                <ul> \
                    <li> \
                        <span class="toolbox-field-header">\
                            <span>► Specials</span>\
                            <input type="button" title="Thêm trường dữ liệu" value="+" class="add-special-field"/>\
                        </span> \
                        <ul style="display: none"> \
                            {{each(i, commonfield) commonFields}} \
                                <li> \
                                <div class="report-toolbox-field" value="${commonfield.value}" title="${commonfield.value}" datatype="${commonfield.datatype}">${commonfield.name}</div>\
                                <input type="button" value="x" title="Xóa trường dữ liệu này" class="delete-field"/>\
                                </li> \
                            {{/each}} \
                        </ul> \
                    </li> \
                </ul> \
            </div>',

        groupFieldPanel: '\
            <div class="group-fields"> \
                <ul> \
                    <li> \
                        <span class="toolbox-field-header">\<span>► Groups</span></span> \
                        <ul style="display: none"> \
                            {{each(i, groupfield) groupFields}} \
                            <li> \
                                <div id="tool_text" class="report-toolbox-field" value="${groupfield.value}" title="${groupfield.value}" datatype="${groupfield.datatype}">${groupfield.name}</div> \
                            </li> \
                            {{/each}} \
                        </ul> \
                    </li> \
                </ul> \
            </div>',

        controlFieldPanel: '\
            <div class="control-fields"> \
                <ul> \
                    <li> \
                        <span class="toolbox-field-header"><span>► Controls</span></span> \
                        <ul style="display: none"> \
                            <li> \
                                <div id="tool_text" class="report-toolbox-field" type="link">Link button</div> \
                            </li> \
                        </ul> \
                    </li> \
                </ul> \
            </div>',

        toolbarPanel: '\
            <div class="toolbar-panel ui-layout-north">\
                <div class="toolbox">\
                    <fieldset>\
                        <legend>Insert</legend>\
                        <a href="#" title="Chèn table vào panel được chọn" class="insert-table" id="insertTable"></a>\
                        <a href="#" title="Chèn text vào vị trí con trỏ" class="insert-text" id="insertText"></a>\
                        <a href="#" title="Chèn hình ảnh vào vị trí con trỏ" class="insert-picture" id="insertImg"></a>\
                    </fieldset>\
                </div>\
                <div class="separate"></div>\
                <div class="properties">\
                    <div class="properties-left">\
                        <div class="properties-left-top">\
                            <select class="prop-font-family" prop="font-family">\
                                <option value=""></option>\
                                <option value="Arial">Arial</option>\
                                <option value="Times New Roman">Times New Roman</option>\
                                <option value="Tahoma">Tahoma</option>\
                                <option value="Verdana">Verdana</option>\
                                <option value="Georgia">Georgia</option>\
                                <option value="Courier New">Courier New</option>\
                                <option value="Trebuchet MS">Trebuchet MS</option>\
                                <option value="Lucida Sans Unicode">Lucida Sans Unicode</option>\
                                <option value="Comic Sans MS">Comic Sans MS</option>\
                            </select>\
                            <select class="prop-font-size" prop="font-size">\
                                <option value="12pt"></option>\
                                <option value="8pt">8</option>\
                                <option value="9pt">9</option>\
                                <option value="10pt">10</option>\
                                <option value="12pt">12</option>\
                                <option value="14pt">14</option>\
                                <option value="16pt">18</option>\
                                <option value="24pt">24</option>\
                                <option value="36pt">36</option>\
                                <option value="48pt">48</option>\
                                <option value="72pt">72</option>\
                            </select>\
                            <div>\
                                <a href="#" title="Viền" class="prop-border">\
                                    <div  class="prop-border-detail">\
                                        <span class="border-below" value="1px solid black" prop="border-bottom" title="Viền dưới">Border below</span>\
                                        <span class="border-above" value="1px solid black" prop="border-top" title="Viền trên">Border above</span>\
                                        <span class="border-left" value="1px solid black" prop="border-left" title="Viền trái">Border left</span>\
                                        <span class="border-right" value="1px solid black" prop="border-right" title="Viền phải">Border right</span>\
                                        <span class="separate-ver" style="background: none; height: 2px; margin: 5px; border-bottom: 1px solid #868585;"></span>\
                                        <span class="border-none" value="none" prop="border" title="Không viền">No border</span>\
                                        <span class="border-all" value="1px solid black" prop="border" title="Viền ngoài">All border</span>\
                                        <span class="border-color" value="black" prop="border-color" title="Màu nền">Border Color</span>\
                                        <span class="border-style" prop="border">Border Style\
                                            <div class="border-style-detail">\
                                                <span class="border-style-solid" value="solid" prop="border-style" title="Solid"></span>\
                                                <span class="border-style-dotted" value="dotted" prop="border-style" title="Dotted"></span>\
                                                <span class="border-style-dashed" value="dashed" prop="border-style" title="Dashed"></span>\
                                                <span class="border-style-double" value="double" prop="border-style" title="Double"></span>\
                                            </div>\
                                        </span>\
                                    </div>\
                                </a>\
                                <a href="#" class="merge-cell" title="Merge Cell"></a>\
                                <a href="#" class="split-cell" title="Split Cell Vertical"></a>\
                                <a href="#" class="split-cell-H" title="Split Cell Horizontal"></a>\
                            </div>\
                        </div>\
                        <div class="properties-left-bottom">\
                            <a href="#" title="In đậm" value="bold" prop="font-weight" class="prop-font-weigh"></a>\
                            <a href="#" title="In nghiêng" value="italic" prop = "font-style" class="prop-font-style"></a>\
                            <a href="#" title="Gạch chân" value="underline" prop = "text-decoration" class="prop-text-decoration"></a>\
                            <a href="#" title="Căn trái" value="left" prop="text-align" class="prop-text-aligh-left"></a>\
                            <a href="#" title="Căn giữa" value="center" prop="text-align" class="prop-text-aligh-center"></a>\
                            <a href="#" title="Căn phải" value="right" prop="text-align" class="prop-text-aligh-right"></a>\
                            <a href="#" title="Căn đều" value="justify" prop="text-align" class="prop-text-aligh-justify"></a>\
                            <a href="#" title="Màu chữ" value="${color}" prop="color" class="prop-color"></a>\
                            <a href="#" title="Màu nền" value="${backgroundColor}" prop="background-color" class="prop-background-color"></a>\
                            <a href="#" title="Custom" class="custom-css" id="CustomCss"></a>\
                        </div>\
                    </div>\
                    <div class="properties-right">\
                        <a href="#" title="insert" class="prop-table-insert">\
                            <div class="prop-table-insert-detail">\
                            <span class="insert-above">Insert Above</span>\
                            <span class="insert-below">Insert Below</span>\
                            <span class="insert-left">Insert Left</span>\
                            <span class="insert-right">Insert Right</span>\
                        </div>\
                        </a>\
                        <a href="#" title="insert" class="prop-table-delete">\
                            <div class="prop-table-delete-detail">\
                            <span class="delete-row" title="Xóa dòng">Delete row</span>\
                            <span class="delete-column" title="Xóa cột">Delete Column</span>\
                            <span class="delete-table" title="Xóa table">Delete table</span>\
                        </div>\
                        </a>\
                    </div>\
                </div>\
                <div class="separate"></div>\
                <div class="layout">\
                    <a href="#" title="Căn trái" class="align-lefts" id="alignLefts"></a>\
                    <a href="#" title="Căn giữa" class="align-centers" id="alignCenters"></a>\
                    <a href="#" title="Căn phải" class="align-rights" id="alignRights"></a>\
                    <a href="#" title="Căn trên" class="align-tops" id="alignTops"></a>\
                    <a href="#" title="Căn giữa" class="align-middles" id="alignMiddles"></a>\
                    <a href="#" title="Căn dưới" class="align-bottoms" id="aLignBottoms"></a>\
                    <a href="#" title="Cùng độ dài" class="same-width" id="sameWidth"></a>\
                    <a href="#" title="Cùng chiều cao" class="same-height" id="sameHeight"></a>\
                    <a href="#" title="Cùng kích thước" class="same-size" id="sameSize"></a>\
                </div>\
                <div class="separate"></div>\
            </div>\
        ',

        reportPanel: '\
            <div class="report-wrapper ui-layout-center"> \
                <div class="report-panel gridview"> \
                    <div class="report-header" style="height: auto">\
                        <div class="report-header-title">Report header</div>\
                            {{html header}}\
                    </div> \
                    <div class="report-detail" style="height: auto">\
                        <div class="group-header">\
                            <div class="report-header-title">Group header\
                                <input class="show-hide-group" type="checkbox" title="Ẩn/hiện group header" name="header"/>\
                            </div>\
                                {{html gHeader}}\
                        </div>\
                        <div class="detail-session">\
                            <div class="report-header-title">Detail</div>\
                                {{html detail}}\
                        </div>\
                        <div class="group-footer">\
                            <div class="report-header-title">Group footer\
                                <input class="show-hide-group" type="checkbox" title="Ẩn/hiện group footer" name="footer"/>\
                            </div>\
                                {{html gFooter}}\
                        </div>\
                    </div> \
                    <div class="report-footer" style="height: auto">\
                        <div class="report-header-title">Report footer</div>\
                            {{html footer}}\
                    </div> \
                </div> \
            </div>',

        optionPanel: '\
            <div class="option-panel">\
                <div class="panel-grid">\
                    <input type="checkbox" class="enable-grid" checked />\
                    <span>Hiển thị lưới</span>\
                </div>\
                <fieldset>\
                    <legend>Định dạng</legend>\
                    <div class="page-size">\
                        <strong><span>Khổ giấy</span></strong>\
                        <select class="page-size-value">\
                            <option value="a4">A4</option>\
                            <option value="a5">A5</option>\
                        </select>\
                    </div>\
                    <div class="page-orientation">\
                        <strong><span>Định hướng</span></strong>\
                        <input type="radio" name="orientation" value="portrait" class="orientation" checked/>\
                        <span>thẳng đứng</span>\
                        <input type="radio" name="orientation" value="landscape" class="orientation"/>\
                        <span> nằm ngang</span>\
                    </div>\
                    <div class="page-margin">\
                        <strong><span>Căn lề</span></strong>\
                        <div><strong><span>Trái</span></strong><input type="text" value="" /></div>\
                        <div><strong><span>Phải</span></strong><input type="text" value="" /></div>\
                        <div><strong><span>Trên</span></strong><input type="text" value="" /></div>\
                        <div><strong><span>Dưới</span></strong><input type="text" value="" /></div>\
                    </div>\
                </fieldset>\
            </div>\
        ',

        tableHeader: '\
                <table class="table-header" cellpadding="0" cellspacing="0" style="border-collapse: collapse"> \
                    <thead> \
                        <tr class="header"> \
                        {{each cols}}\
                            <th></th> \
                        {{/each}}\
                        </tr> \
                    </thead> \
                </table>\
            ',

        tableDetail: '\
            <table class="table-detail" cellpadding="0" cellspacing="0" style="border-collapse: collapse"> \
                <tbody> \
                    <tr class="${className}"> \
                    {{each cols}}\
                        <td></td> \
                    {{/each}}\
                    </tr> \
                </tbody> \
            </table>',

        tableFooter: '\
            <table class="table-footer" cellpadding="0" cellspacing="0" style="border-collapse: collapse">\
                <tfoot>\
                    <tr class="footer">\
                    {{each cols}}\
                        <td></td> \
                    {{/each}}\
                    </tr>\
               </tfoot>\
            </table>',

        addField: '\
            <div class="add-field">\
                <div>\
                    <strong><span>Tên: </span></strong><input type="text" class="name"/>\
                </div>\
                <div>\
                    <strong><span>Giá trị: </span></strong><input type="text" class="value"/>\
                </div>\
                <div>\
                    <strong><span>Kiểu giá trị: </span></strong>\
                    <select class="datatype">\
                        <option value="string">Kiểu chuỗi</option>\
                        <option value="number">Kiểu số</option>\
                        <option value="curency">Kiểu tiền tệ</option>\
                        <option value="datetime">Kiểu ngày tháng</option>\
                    </select>\
                </div>\
                <div>\
                    <strong><span>Sắp xếp: </span></strong><input type="checkbox" class="sort"/>\
                </div>\
                <div>\
                    <input type="button" value="Cancel" class="cancel"/>\
                    <input type="button" value="OK" />\
                </div>\
            </div>\
        ',

        addControl: '\
            <div class="add-control">\
                <div style="color: red; font-weight: bold;">Chú ý: câu sql bắt buộc phải select ra DocumentCopyId</div>\
                <div>\
                    <strong><span>Tên: </span></strong><input type="text" class="name"/>\
                </div>\
                <div>\
                    <strong><span>Chọn hành động: </span></strong>\
                    <select class="select-action">\
                        <option value="viewDocument(#DocumentCopyId#, #Compendium#)">Xem</option>\
                        <option value="deleteDocument(#DocumentCopyId#)">Xóa</option>\
                        <option value="editDocument(#DocumentCopyId#)">Sửa</option>\
                        <option value="sort" class="sort-document">Sắp xếp</option>\
                        <option value="downAttachment(#DocumentCopyId#)">Tải file đính kèm</option>\
                    </select>\
                </div>\
                <div class="sort-value" style="display: none">\
                    <strong><span>Chọn dữ liệu sắp xếp: </span></strong>\
                    <input type="text" class="sort-by"/>\
                    <select class="field-collection"></select>\
                </div>\
                <div>\
                    <input type="button" value="Cancel" class="cancel"/>\
                    <input type="button" value="OK" />\
                </div>\
            </div>\
        ',
        properties: '\
            <div class="format-object">\
                {{if datatype == "number" || datatype == "currency" }}\
                    <div class="number-format">\
                        <div class="format-partern">\
                            <fieldset>\
                                <legend>Number format</legend>\
                                <ul>\
                                    <li val="N" sample="123456.789 -> 123.456,79">Default system format</li>\
                                    <li val="F0" sample="123456.789 -> 123456">1234</li>\
                                    <li val="N0" sample="123456.789 -> 123.456">1,234</li>\
                                    <li val="F2" sample="123456.789 -> 123456,79">1234.00</li>\
                                    <li val="N2" sample="123456.789 -> 123.456,79">1,234.00</li>\
                                    <li val="F4" sample="123456.789 -> 123456,7890">1234.0000</li>\
                                    <li val="N4" sample="123456.789 -> 123.456,7890">1,234.0000</li>\
                                </ul>\
                            </fieldset>\
                        </div>\
                        <div class="currency-symbol">\
                            <fieldset>\
                                <legend>Using currency format</legend>\
                                <input type="checkbox" value="C2" sample="123456.789 -> 123.456,79 ₫"><span>Using currency</span>\
                            </fieldset>\
                        </div>\
                    </div>\
                {{else datatype == "datetime"}}\
                    <div class="datatime-format">\
                        <div class="format-partern">\
                            <fieldset>\
                                <legend>Datetime Format</legend>\
                                <ul>\
                                    <li val="D" sample="6/15/2009 1:45:30 PM -> Monday, June 15, 2009">Default system long format</li>\
                                    <li val="d" sample="6/15/2009 1:45:30 PM -> 6/15/2009">Default system sort format</li>\
                                    <li val="g" sample="6/15/2009 1:45:30 PM -> 6/15/2009 1:45 PM">6/15/2009 1:45 PM</li>\
                                    <li val="G" sample="6/15/2009 1:45:30 PM -> 6/15/2009 1:45:30 PM"> 6/15/2009 1:45:30 PM</li>\
                                    <li val="f" sample="6/15/2009 1:45:30 PM -> Monday, June 15, 2009 1:45 PM">Monday, June 15, 2009 1:45 PM</li>\
                                    <li val="F" sample="6/15/2009 1:45:30 PM -> Monday, June 15, 2009 1:45:30 PM">Monday, June 15, 2009 1:45:30 PM </li>\
                                    <li val="M" sample="6/15/2009 1:45:30 PM -> June 15">June 15</li>\
                                    <li val="t" sample="6/15/2009 1:45:30 PM -> 1:45 PM">1:45 PM</li>\
                                    <li val="T" sample="6/15/2009 1:45:30 PM -> 1:45:30 PM">1:45:30 PM </li>\
                                    <li val="u" sample="6/15/2009 1:45:30 PM -> 2009-06-15 20:45:30Z"> 2009-06-15 20:45:30Z</li>\
                                    <li val="Y" sample="6/15/2009 1:45:30 PM -> June, 2009">June, 2009</li>\
                                    <li val="MM/dd/yyyy" sample="6/15/2009 1:45:30 PM -> 6/15/2009">03/12/2012</li>\
                                    <li val="MM" sample="6/15/2009 1:45:30 PM -> 06">06</li>\
                                </ul>\
                            </fieldset>\
                        </div>\
                    </div>\
                {{/if}}\
                    <div class="preview">\
                        <fieldset>\
                            <legend>Preview\
                            </legend>\
                            <p></p>\
                        </fieldset>\
                    </div>\
                <div>\
                    <input type="button" value="Cancel" class="cancel"/>\
                    <input type="button" value="Ok" class="ok"/>\
                </div>\
            </div>\
        ',
        summary: '\
            <div class="summary">\
                <div>\
                    <p>Chọn trường dữ liệu</p>\
                    <select class="select-field">\
                    </select>\
                </div>\
                <div>\
                    <p>Chọn summary</p>\
                    <select class="select-summary">\
                        <option value="Sum" for="number">Tổng</option>\
                        <option value="Average" for="number">Trung bình</option>\
                        <option value="TotalWith" for="number">Tổng với</option>\
                        <option value="SubWith" for="number">Hiệu với</option>\
                        <option value="Max" for="number">Lớn nhất</option>\
                        <option value="Min" for="number">Nhỏ nhất</option>\
                        <option value="PercenWith" for="number">Phần trăm với</option>\
                        <option value="Count" for="string">Số lượng</option>\
                    </select>\
                    <select class="select-field-with" style="display: none;">\
                    </select>\
                </div>\
                <div style="display: none">\
                    <p>Lấy dữ liệu trong</p>\
                    <select>\
                        <option value="row">Dữ liệu trong row</option>\
                        <option value="group">Dữ liệu trong group</option>\
                        <option value="report">Dữ liệu trong toàn bộ báo cáo</option>\
                    </select>\
                </div>\
                <div style="text-align: right">\
                    <input type="button" value="Ok" />\
                    <input type="button" value="Cancel" class="cancel"/>\
                </div>\
            </div>\
        ',
        customCss: '\
            <div class="custom-css-form summary" style="color: fahkfe">\
                <div>\
                    <p>Thuộc tính</p>\
                    <select class="select-field">\
                        <option value="color">Color</option>\
                        <option value="backgroundColor">Background Color</option>\
                    </select>\
                </div>\
                <div>\
                    <p>Giá trị</p>\
                    <textarea row="5" col="20" style="width: 100%;" placeholder="Ví dụ: @(DateAppoint == DateTime.Now? blue : green)"/>\
                </div>\
                <div style="text-align: right">\
                    <input type="button" value="Ok" />\
                    <input type="button" value="Cancel" class="cancel"/>\
                </div>\
            </div>\
        '
    };
})(eReport = eReport || {});