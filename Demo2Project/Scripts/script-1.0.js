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
  $(".form-group input[type=text]:not(:disabled),textarea").first().focus();
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

$(function()
{
  $('#datepicker').datepicker({ 
  }).on('changeDate', function (e)
  {
    $('#datepicker').parent().find('#datepicker-hidden').val(e.format('yyyy/mm/dd'));
  });
  $('.input-daterange').datepicker({
    forceParse: false,
    startDate: "05/10/2014",
    todayBtn: "linked",
    calendarWeeks: true,
    todayHighlight: true,
  })
});

var substringMatcher = function(strs) {
  return function findMatches(q, cb) {
    var matches, substringRegex;

    // an array that will be populated with substring matches
    matches = [];

    // regex used to determine if a string contains the substring `q`
    substrRegex = new RegExp(q, 'i');

    // iterate through the pool of strings and for any string that
    // contains the substring `q`, add it to the `matches` array
    $.each(strs, function(i, str) {
      if (substrRegex.test(str)) {
        // the typeahead jQuery plugin expects suggestions to a
        // JavaScript object, refer to typeahead docs for more info
        matches.push({ value: str });
      }
    });

    cb(matches.slice(0, 5));
  };
};
var states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California',
  'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii',
  'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana',
  'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota',
  'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire',
  'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota',
  'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island',
  'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
  'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'
];
//var states = ['New York']
//// constructs the suggestion engine
//var states = new Bloodhound({
//  datumTokenizer: function (obj) { return [ 'New', 'ew', 'w', 'York','ork','rk','k' ]; },
//  queryTokenizer: Bloodhound.tokenizers.whitespace,
//  // `states` is an array of state names defined in "The Basics"
//  local: $.map(states, function (state) { return { value: state }; }),
//  limit: 5
//});

//// kicks off the loading/processing of `local` and `prefetch`
//states.initialize();

$('#typeahead').typeahead({
  hint: true,
  highlight: true,
  minLength: 1
},
{
  name: 'states',
  displayKey: 'value',
  // `ttAdapter` wraps the suggestion engine in an adapter that
  // is compatible with the typeahead jQuery plugin
  source: substringMatcher(states)
});
