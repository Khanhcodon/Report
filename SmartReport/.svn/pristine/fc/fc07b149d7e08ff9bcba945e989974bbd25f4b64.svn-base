$('#deliverables').jstree({
    'core': {
        themes: {
            icons: false,
            name: 'proton',
            responsive: false
        },
        'data': [{ "id": "publish", "parent": "#", "text": "publish", "icon": "jstree-folder", "a_attr": { "href": "#" }, "state": { "opened": true } }, { "id": "publish/MD5SUM.txt", "parent": "publish", "text": "MD5SUM.txt (0MB)", "icon": "jstree-file" }, { "id": "publish/SHA1SUM.txt", "parent": "publish", "text": "SHA1SUM.txt (0MB)", "icon": "jstree-file" }, { "id": "publish/VCSA-all-6.0.0-2367421.iso", "parent": "publish", "text": "VCSA-all-6.0.0-2367421.iso (2723MB)", "icon": "jstree-file" }, { "id": "publish/VIMSetup-all-6.0.0-2367421.iso", "parent": "publish", "text": "VIMSetup-all-6.0.0-2367421.iso (2610MB)", "icon": "jstree-file" }]
    },
    'search': {
        'case_insensitive': true,
        'show_only_matches': true
    },
    'plugins': ['search', 'checkbox', 'wholerow']
}).on('search.jstree', function (nodes, str, res) {
    if (str.nodes.length === 0) {
        $('#deliverables').jstree(true).hide_all();
    }
})

$('#deliverable_search').keyup(function () {
    $('#deliverables').jstree(true).show_all();
    $('#deliverables').jstree('search', $(this).val());
});
