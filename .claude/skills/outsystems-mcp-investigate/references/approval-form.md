# Story approval form markup

Markup for the `widget_code` of `mcp__visualize__show_widget` (elicitation shell). It renders inline in the conversation. Never save it as an `.html` file or publish it as an Artifact.

How it works:

- One `.elicit-group` per proposed story. `data-name` = the story ID, so the payload reads `US6: Approve`, `TT5: Reject`, `TT5 (other): <reason>`.
- The "Reject" pill carries `data-other`, so selecting it reveals the paired `.elicit-other` reason box. The reason is optional.
- Priority badge next to the ID: High = `var(--bg-danger)` / `var(--text-danger)`; Medium = `var(--bg-warning)` / `var(--text-warning)`; Low = `var(--bg-accent)` / `var(--text-accent)`. Never pre-select a pill.
- Under the title: type and size, modules, the description in at most two lines, then "Covers: F03, F08" in muted text.
- The header SVG is fixed chrome from the elicitation guide: copy it byte-for-byte. Header text: `"<scope> · investigation stories"`.
- Footer, left to right: Skip (required by the shell), "Approve all", "Reject all", then the submit button labelled "Submit stories". The bulk buttons carry `data-bulk="Approve"` / `data-bulk="Reject"` (the pill `data-value` they select) and are wired by the `<script>` after the form: copy it as is. It clicks the matching pill in every row that isn't already on it, so the shell's own selection runs and "Reject all" reveals every reason box. The user can still change single rows afterwards. The script is the one exception to the shell's no-script rule.
- The widget rules forbid HTML comments: don't add any.

```html
<h2 class="sr-only">Proposed stories from the investigation, each with an Approve or Reject answer and an optional reason when rejected.</h2>
<form class="elicit">
  <div class="elicit-header">
    <svg viewBox="0 0 20 20" fill="currentColor"><path d="M11.586 2a1.5 1.5 0 0 1 1.06.44l2.914 2.914a1.5 1.5 0 0 1 .44 1.06V16.5a1.5 1.5 0 0 1-1.5 1.5h-9a1.5 1.5 0 0 1-1.492-1.347L4 16.5v-13A1.5 1.5 0 0 1 5.5 2zM5.5 3a.5.5 0 0 0-.5.5v13a.5.5 0 0 0 .5.5h9a.5.5 0 0 0 .5-.5V7h-2.5A1.5 1.5 0 0 1 11 5.5V3zm7.04 10.304a.5.5 0 0 1 .92.392c-.295.69-.871 1.304-1.66 1.304-.487 0-.892-.234-1.2-.574-.309.34-.713.574-1.2.574-.486 0-.892-.233-1.2-.574-.31.34-.714.574-1.2.574a.5.5 0 0 1 0-1c.212 0 .52-.18.74-.696l.034-.067a.5.5 0 0 1 .886.067c.221.516.528.696.74.696.213 0 .52-.18.74-.696l.035-.067a.5.5 0 0 1 .885.067c.22.516.527.696.74.696s.519-.18.74-.696m0-4a.5.5 0 0 1 .92.392c-.295.69-.871 1.304-1.66 1.304-.487 0-.892-.234-1.2-.574-.309.34-.713.574-1.2.574-.486 0-.892-.233-1.2-.574-.31.34-.714.574-1.2.574a.5.5 0 0 1 0-1c.212 0 .52-.18.74-.696l.034-.067a.5.5 0 0 1 .886.067c.221.516.528.696.74.696.213 0 .52-.18.74-.696l.035-.067a.5.5 0 0 1 .885.067c.22.516.527.696.74.696s.519-.18.74-.696M12 5.5a.5.5 0 0 0 .5.5h2.293L12 3.207z"/></svg>
    <span>OrderCard · investigation stories</span>
  </div>
  <div class="elicit-body">

    <div class="elicit-group" style="padding:12px 14px; border:0.5px solid var(--border); border-radius:12px;">
      <label class="elicit-question">
        <span style="font-size:12px; padding:2px 8px; border-radius:var(--radius); background:var(--bg-danger); color:var(--text-danger);">High</span>
        <span style="font-family:var(--font-mono); font-size:13px;">US6 · [Block] OrderCard: show save errors in the order editor</span>
      </label>
      <div style="font-size:13px; color:var(--text-secondary); line-height:1.6; margin:4px 0 8px;">
        Story · M · Sales_CS, Sales<br>
        As a sales rep I want to see why an order couldn't be saved, so that I can fix it without guessing.<br>
        <span style="color:var(--text-muted);">Covers: F01, F04</span>
      </div>
      <div class="elicit-pills" data-name="US6" data-multi="false">
        <button type="button" class="elicit-pill" data-value="Approve" data-accent="success">Approve</button>
        <button type="button" class="elicit-pill" data-value="Reject" data-other>Reject</button>
      </div>
      <input type="text" class="elicit-other" data-for="US6" placeholder="Why reject it? (optional)" hidden>
    </div>

    <div class="elicit-group" style="padding:12px 14px; border:0.5px solid var(--border); border-radius:12px;">
      <label class="elicit-question">
        <span style="font-size:12px; padding:2px 8px; border-radius:var(--radius); background:var(--bg-accent); color:var(--text-accent);">Low</span>
        <span style="font-family:var(--font-mono); font-size:13px;">TT5 · [Tech] Sales_CS: add missing action descriptions</span>
      </label>
      <div style="font-size:13px; color:var(--text-secondary); line-height:1.6; margin:4px 0 8px;">
        Tech task · S · Sales_CS<br>
        Six public server actions have no description, so the consumer sees nothing in Service Studio's tooltips.<br>
        <span style="color:var(--text-muted);">Covers: F09</span>
      </div>
      <div class="elicit-pills" data-name="TT5" data-multi="false">
        <button type="button" class="elicit-pill" data-value="Approve" data-accent="success">Approve</button>
        <button type="button" class="elicit-pill" data-value="Reject" data-other>Reject</button>
      </div>
      <input type="text" class="elicit-other" data-for="TT5" placeholder="Why reject it? (optional)" hidden>
    </div>

  </div>
  <div class="elicit-footer">
    <button type="button" class="elicit-skip">Skip</button>
    <button type="button" data-bulk="Approve" style="background:transparent; border:0.5px solid var(--border); border-radius:var(--radius); padding:6px 12px; font-size:13px; color:var(--text-primary); cursor:pointer;">Approve all</button>
    <button type="button" data-bulk="Reject" style="background:transparent; border:0.5px solid var(--border); border-radius:var(--radius); padding:6px 12px; font-size:13px; color:var(--text-primary); cursor:pointer;">Reject all</button>
    <button type="button" class="elicit-submit">Submit stories</button>
  </div>
</form>
<script>
document.querySelectorAll('[data-bulk]').forEach(function (b) {
  b.addEventListener('click', function () {
    var v = b.getAttribute('data-bulk');
    document.querySelectorAll('.elicit-pills').forEach(function (g) {
      var p = g.querySelector('.elicit-pill[data-value="' + v + '"]');
      if (p && p.getAttribute('aria-pressed') !== 'true') p.click();
    });
  });
});
</script>
```
