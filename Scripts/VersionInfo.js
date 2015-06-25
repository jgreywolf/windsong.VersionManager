$(function () {
    $('#readOnlyContainer').buttonset();

    updateContentDisplay(isReadOnly);

    $('#lblRemoveReadOnly').on('click', function () {
        setContentState(false);
    });

    $('#lblSetReadOnly').on('click', function (e) {
        updateContentDisplay(true);
        setContentState(true);
        e.stopPropagation();
        e.preventDefault();
    });
});

function updateContentDisplay(isReadOnly) {
    var $editDiv = $('.edit-item');
    $editDiv.each(function () {
        var $this = $(this);
        if (isReadOnly) {
            $('#btnSetReadOnly').click();
            $this.wrap('<div class="disabled-content-wrapper"></div>');
            $('[name="Body.Text"]').closest('fieldset').addClass('readOnly');
        } else {
            $('#btnRemoveReadOnly').click();
            if ($this.parent().hasClass('.disabled-content-wrapper')) {
                $this.unwrap();
            }
            $('[name="Body.Text"]').closest('fieldset').removeClass('readOnly');
        }

        $('.disabled-content-wrapper button').toggleClass('readOnly');
        $('.disabled-content-wrapper span.button').toggleClass('readOnly');
        $('.disabled-content-wrapper a.media-library-picker-remove').toggleClass('readOnly');

        $('.disabled-content-wrapper input').prop('readonly', isReadOnly);
        $('.disabled-content-wrapper select').prop('disabled', isReadOnly);
        $('.disabled-content-wrapper textarea').prop('readonly', isReadOnly);
        $('.disabled-content-wrapper textarea').prop('disabled', isReadOnly);

        $('.disabled-content-wrapper button').prop('disabled', isReadOnly);
        $('.disabled-content-wrapper input[type="checkbox"]').prop('disabled', isReadOnly);
        $('.disabled-content-wrapper input[type="radio"]').not('.dontdisable').prop('disabled', isReadOnly);
    });
}

function setContentState(isReadOnly) {
    var contentId = $('#ReadOnlySettings_ContentId').val();
    var token = $('input[name="__RequestVerificationToken"]', $('form')).val();

    var url = '/Windsong.VersionManager/Admin/SetReadOnlyState/';
    $.ajax({
        type: 'POST',
        url: url,
        data: {
            __RequestVerificationToken: token,
            contentId: contentId,
            isReadOnly: isReadOnly
        },
        success: function () {
            location.reload();
        },
        error: function (jqXhr, status, error) {
            updateContentDisplay(!isReadOnly);

            if (jqXhr.status == 403) {
                alert('You do not have permissions to manage the state of this content');
            } else if (jqXhr.status == 404) {
                alert('Requested resource not found. [404]');
            } else if (jqXhr.status == 500) {
                alert('Internal Server Error [500].');
            } else {
                alert('Uncaught Error.\n' + jqXhr.responseText);
            }
        }
    });
}

function openDialogWindow() {
    var $this = $(this);
    $.colorbox({
        href: $this.url,
        iframe: true,
        reposition: true,
        width: "100%",
        height: "100%",
        onClosed: function() {
            $('html, body').css('overflow', '');
        },
        onLoad: function () { // hide the scrollbars from the main window
            $('html, body').css('overflow', 'hidden');
        }
    });
}
    