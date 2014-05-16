///* Bootstrap model focus issue workaround */
//// Since confModal is essentially a nested modal it's enforceFocus method
//// must be no-op'd or the following error results 
//// "Uncaught RangeError: Maximum call stack size exceeded"
//// But then when the nested modal is hidden we reset modal.enforceFocus
//var enforceModalFocusFn = $.fn.modal.Constructor.prototype.enforceFocus;

//$.fn.modal.Constructor.prototype.enforceFocus = function () { };

//$confModal.on('hidden', function () {
//  $.fn.modal.Constructor.prototype.enforceFocus = enforceModalFocusFn;
//});

//$confModal.modal({ backdrop: false });

// Flyin for content
$(document).ready(function () {
  var form = $('.body-content');
  var marginLeft = parseInt(form.css('marginLeft'));
  var marginRight = parseInt(form.css('marginRight'));
  form.css({ 'opacity': 0, 'marginLeft': marginLeft + 20, 'marginRight': marginRight - 20 }).animate({
    'opacity': 1,
    'marginLeft': marginLeft,
    'marginRight': marginRight
  }, 300, 'swing', function () {
    form.css({ 'marginLeft': '', 'marginRight': '' });
  });
});

// Validation
$(document).ready(function()
{
  $('.input-validation-error').each(function ()
  {
    var errorLabel = $(document.createElement('span'));
    errorLabel.addClass('glyphicon glyphicon-exclamation-sign form-control-feedback');
    $(this).after(errorLabel);
    $(this).closest('.form-group').addClass('has-error has-feedback');
  });
});

// Focus
$(document).ready(function () {
  $("input[type=text]:not(:disabled),textarea").first().focus();
});

// Submitting
$(document).ready(function ()
{
  $('input[type="submit"]').click(function () {
    $(this).button('loading')
  });
});

// Message
(function(Demo2Project, $, undefined) {
  (function(Api, $, undefined) {
    Api.ShowMessage = function (url) {
      var json = $.getJSON(url, function (result)
      {
        var message = $('.message').hide().addClass(result.typeClass);
        var container = $(document.createElement('div')).addClass('message_list container').appendTo(message);
        var close = $(document.createElement('i')).addClass('message_close glyphicon glyphicon-remove');
        $(result.messageLines).each(function (i, messageLine)
        {
          var item = $(document.createElement('div')).addClass('message_item').html(messageLine).appendTo(container);
          if (i === 0) {
            close.appendTo(item);
          }
        });
        message.stop().fadeIn(200).delay(3000).fadeOut(200);
        message.off('mouseenter mouseleave').on('mouseenter mouseleave', function () {
          $(this).clearQueue();
        }, function () {
          $(this).delay(1000).fadeOut(200);
        });
        close.off('click').on('click', function () {
          message.clearQueue().fadeOut(200);
        });
      });
    }
  }(Demo2Project.Api = Demo2Project.Api || {}, jQuery));
}(window.Demo2Project = window.Demo2Project || {}, jQuery));