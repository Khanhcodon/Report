﻿/******************************************************************************

Copyright (c) 2016, Highsoft

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

******************************************************************************/

(function () {
    if (typeof window['CKEDITOR'] === 'undefined') {
        return;
    }

    //This is very dirty
    highed.dom.ap(document.head, highed.dom.cr('style', '', '.cke_button__highcharts_icon{background-image:url(\'data:image/svg+xml;base64,' + btoa('<?xml version="1.0" encoding="utf-8"?><!-- Generator: Adobe Illustrator 16.0.3, SVG Export Plug-In . SVG Version: 6.00 Build 0)  --><!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"><svg version="1.1" id="logo-highcharts" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="16" height="16" viewBox="0 0 225 225" xml:space="preserve"><g id="symbol" transform="translate(10,13.41400146484375) scale(3)"><polygon fill="#8087E8" points="41.53900146484375,0 30.315000534057617,26.209999084472656 15.566999435424805,60.650997161865234 49.85900115966797,46.16499710083008 68.02299499511719,38.49300003051758 " id="svg_2" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#30426B" points="47.25799560546875,31.729999542236328 49.86000061035156,46.16499710083008 68.02400207519531,38.49300003051758 " id="svg_3" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#6699A1" points="41.53900146484375,0 47.25799560546875,31.730998992919922 68.02299499511719,38.49300003051758 " id="svg_4" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#78758C" points="47.25799560546875,31.729999542236328 68.02400207519531,38.49300003051758 30.31599998474121,26.208999633789062 15.566999435424805,60.650997161865234 49.86000061035156,46.16499710083008 " id="svg_5" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon>                      <polygon fill="#A3EDBA" points="15.566999435424805,60.650997161865234 30.315000534057617,26.209999084472656 0,16.334999084472656 " id="svg_6" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#6699A1" points="49.86000061035156,46.16499710083008 53.185997009277344,64.6099967956543 68.02400207519531,38.49300003051758 " id="svg_7" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#8087E8" points="41.53900146484375,0 30.315000534057617,26.209999084472656 47.25799560546875,31.730998992919922 " id="svg_8" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon></g></svg>') + "') !important;}"));
    

    CKEDITOR.plugins.add('highcharts', {
        init: function (editor) {

            var modal = highed.ModalEditor(false, {
                    features: 'import templates customize welcome done',
                    allowDone: true
            }, function (chart) {
                    var t = chart;
                    var html = chart.export.html(true);
                    try {
                     editor.insertHtml('<div contenteditable="false">' + html + '</div><p></p>');                
                    } catch (e) {
                        eGovMessage.notification("Bạn chưa chọn vị trí lưu chart", eGovMessage.messageTypes.error,true);
                        modal.hide();
                    } 
                })
            ;        

            editor.addCommand('insertHighcharts', {               
                allowedContent: 'script[type,src];div[id,style]a[*];altGlyph[*];altGlyphDef[*];altGlyphItem[*];animate[*];animateColor[*];animateMotion[*];animateTransform[*];circle[*];clipPath[*];color-profile[*];cursor[*];defs[*];desc[*];ellipse[*];feBlend[*];feColorMatrix[*];feComponentTransfer[*];feComposite[*];feConvolveMatrix[*];feDiffuseLighting[*];feDisplacementMap[*];feDistantLight[*];feFlood[*];feFuncA[*];feFuncB[*];feFuncG[*];feFuncR[*];feGaussianBlur[*];feImage[*];feMerge[*];feMergeNode[*];feMorphology[*];feOffset[*];fePointLight[*];feSpecularLighting[*];feSpotLight[*];feTile[*];feTurbulence[*];filter[*];font[*];font-face[*];font-face-format[*];font-face-name[*];font-face-src[*];font-face-uri[*];foreignObject[*];g[*];glyph[*];glyphRef[*];hkern[*];image[*];line[*];linearGradient[*];marker[*];mask[*];metadata[*];missing-glyph[*];mpath[*];path[*];pattern[*];polygon[*];polyline[*];radialGradient[*];rect[*];script[*];set[*];stop[*];style[*];svg[*];switch[*];symbol[*];text[*];textPath[*];title[*];tref[*];tspan[*];use[*];view[*];vkern[*]',
                requiredContent: 'div[id,style]a[*];altGlyph[*];altGlyphDef[*];altGlyphItem[*];animate[*];animateColor[*];animateMotion[*];animateTransform[*];circle[*];clipPath[*];color-profile[*];cursor[*];defs[*];desc[*];ellipse[*];feBlend[*];feColorMatrix[*];feComponentTransfer[*];feComposite[*];feConvolveMatrix[*];feDiffuseLighting[*];feDisplacementMap[*];feDistantLight[*];feFlood[*];feFuncA[*];feFuncB[*];feFuncG[*];feFuncR[*];feGaussianBlur[*];feImage[*];feMerge[*];feMergeNode[*];feMorphology[*];feOffset[*];fePointLight[*];feSpecularLighting[*];feSpotLight[*];feTile[*];feTurbulence[*];filter[*];font[*];font-face[*];font-face-format[*];font-face-name[*];font-face-src[*];font-face-uri[*];foreignObject[*];g[*];glyph[*];glyphRef[*];hkern[*];image[*];line[*];linearGradient[*];marker[*];mask[*];metadata[*];missing-glyph[*];mpath[*];path[*];pattern[*];polygon[*];polyline[*];radialGradient[*];rect[*];script[*];set[*];stop[*];style[*];svg[*];switch[*];symbol[*];text[*];textPath[*];title[*];tref[*];tspan[*];use[*];view[*];vkern[*]',
                exec: function (editor) {
                    modal.show();                    
                }
            });

            editor.ui.addButton('Highcharts', {
                label: 'Highcharts',
                command: 'insertHighcharts'
                //style: 'background-image:url(\'data:image/svg+xml;base64,' + btoa('<?xml version="1.0" encoding="utf-8"?><!-- Generator: Adobe Illustrator 16.0.3, SVG Export Plug-In . SVG Version: 6.00 Build 0)  --><!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"><svg version="1.1" id="logo-highcharts" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="16" height="16" viewBox="0 0 225 225" xml:space="preserve"><g id="symbol" transform="translate(10,13.41400146484375) scale(3)"><polygon fill="#8087E8" points="41.53900146484375,0 30.315000534057617,26.209999084472656 15.566999435424805,60.650997161865234 49.85900115966797,46.16499710083008 68.02299499511719,38.49300003051758 " id="svg_2" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#30426B" points="47.25799560546875,31.729999542236328 49.86000061035156,46.16499710083008 68.02400207519531,38.49300003051758 " id="svg_3" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#6699A1" points="41.53900146484375,0 47.25799560546875,31.730998992919922 68.02299499511719,38.49300003051758 " id="svg_4" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#78758C" points="47.25799560546875,31.729999542236328 68.02400207519531,38.49300003051758 30.31599998474121,26.208999633789062 15.566999435424805,60.650997161865234 49.86000061035156,46.16499710083008 " id="svg_5" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon>                      <polygon fill="#A3EDBA" points="15.566999435424805,60.650997161865234 30.315000534057617,26.209999084472656 0,16.334999084472656 " id="svg_6" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#6699A1" points="49.86000061035156,46.16499710083008 53.185997009277344,64.6099967956543 68.02400207519531,38.49300003051758 " id="svg_7" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon><polygon fill="#8087E8" points="41.53900146484375,0 30.315000534057617,26.209999084472656 47.25799560546875,31.730998992919922 " id="svg_8" stroke-width="0" stroke="#383836" fill-opacity="1" stroke-linejoin="round"></polygon></g></svg>') + "');"
            });
        },
        credits: {
            enabled: false
        }
    });
})();
