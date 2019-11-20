

tinymce.init({
    selector: '#micro',
    height: 400,
    resize: false,
    plugins:'wordcount link autosave',
    toolbar: ' bold italic link autosave  underline strikethrough superscript subscript| fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | numlist bullist | forecolor backcolor | emoticons | preview | link codesample ',
    toolbar_sticky: true,
    menubar: false,
    init_instance_callback: function (editor) {
        $(editor.getContainer()).find('button.tox-statusbar__wordcount').click();  // if you use jQuery
    },
});


tinymce.init({
    selector: '#mega',
    resize: false,

    plugins: 'preview lineheight paste importcss searchreplace autolink autosave save directionality  visualblocks visualchars  image link media  codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern noneditable help charmap  emoticons',

    init_instance_callback: function (editor) {
        $(editor.getContainer()).find('button.tox-statusbar__wordcount').click();  // if you use jQuery
    },

    menubar: 'edit view  table ',
    toolbar: 'undo redo | bold italic underline strikethrough superscript subscript| fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify |image media|  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | preview | image media template link anchor codesample | ltr rtl| outdent indent ',
    toolbar_sticky: true,
    autosave_ask_before_unload: true,
    autosave_interval: "30s",
    autosave_prefix: "{path}{query}-{id}-",
    autosave_restore_when_empty: false,
    autosave_retention: "2m",
    image_advtab: true,
    content_css: [
        '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
        '//www.tiny.cloud/css/codepen.min.css'
    ],
    height: 800,
    image_caption: true,
    quickbars_selection_toolbar: 'bold italic | quicklink h1 link h2 h3 blockquote quickimage quicktable',
    toolbar_drawer: 'sliding',
});


