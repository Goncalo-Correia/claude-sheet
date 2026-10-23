# Review form markup

Markup for the `widget_code` of `mcp__visualize__show_widget` (elicitation shell). It renders inline in the conversation. Never save it as an `.html` file or publish it as an Artifact.

How it works:

- One form, sections in this order: Acceptance criteria (`A01`…), Changes (`C01`…), Conventions (`N01`…, when the repository has `docs/conventions.md`), then Implementation plan (`P01`…) only when the plan check wasn't already approved by `outsystems-mcp-implementation`. Each section starts with a small heading div showing its row count.
- One `.elicit-group` per row. `data-name` = the row ID, so the payload reads `A01: Yes`, `A02: No`, `A02 (other): <reason>`.
- The "No" pill carries `data-other`, so selecting it reveals the paired `.elicit-other` reason box.
- Criteria and plan rows carry Claude's tag badge next to the ID: Approved = `var(--bg-success)` / `var(--text-success)`; Rejected = `var(--bg-danger)` / `var(--text-danger)`. Under it: the reason (Rejected) or the evidence (Approved), plus "Test in the browser: <screen, user, steps>." when it applies. Never pre-select a pill from the tag.
- Change rows have no tag. Change-type badges: Created = success colours, Modified = accent colours, Deleted / Validation error = danger colours.
- Rows fixed in this round use the yellow style shown on A02: background `var(--bg-warning)`, border `var(--border-warning)` and a "Fixed in round N" note.
- The header SVG is fixed chrome from the elicitation guide: copy it byte-for-byte.
- Footer, left to right: Skip (required by the shell), "Yes to all", "No to all", then the submit button labelled "Submit review". The bulk buttons carry `data-bulk="Yes"` / `data-bulk="No"` (the pill `data-value` they select) and are wired by the `<script>` after the form: copy it as is. It clicks the matching pill in every row that isn't already on it, so the shell's own selection runs and "No to all" reveals every reason box. The user can still change single rows afterwards. The script is the one exception to the shell's no-script rule.
- The widget rules forbid HTML comments: don't add any.

The conventions section and the plan section, when included, use the same row markup as the criteria (tag badge + reason or evidence), with IDs `N01`, `N02`, … and `P01`, `P02`, ….

```html
<h2 class="sr-only">Review form listing the acceptance criteria and every change made to the module, each with a Yes or No answer and a reason when No.</h2>
<form class="elicit">
  <div class="elicit-header">
    <svg viewBox="0 0 20 20" fill="currentColor"><path d="M11.586 2a1.5 1.5 0 0 1 1.06.44l2.914 2.914a1.5 1.5 0 0 1 .44 1.06V16.5a1.5 1.5 0 0 1-1.5 1.5h-9a1.5 1.5 0 0 1-1.492-1.347L4 16.5v-13A1.5 1.5 0 0 1 5.5 2zM5.5 3a.5.5 0 0 0-.5.5v13a.5.5 0 0 0 .5.5h9a.5.5 0 0 0 .5-.5V7h-2.5A1.5 1.5 0 0 1 11 5.5V3zm7.04 10.304a.5.5 0 0 1 .92.392c-.295.69-.871 1.304-1.66 1.304-.487 0-.892-.234-1.2-.574-.309.34-.713.574-1.2.574-.486 0-.892-.233-1.2-.574-.31.34-.714.574-1.2.574a.5.5 0 0 1 0-1c.212 0 .52-.18.74-.696l.034-.067a.5.5 0 0 1 .886.067c.221.516.528.696.74.696.213 0 .52-.18.74-.696l.035-.067a.5.5 0 0 1 .885.067c.22.516.527.696.74.696s.519-.18.74-.696m0-4a.5.5 0 0 1 .92.392c-.295.69-.871 1.304-1.66 1.304-.487 0-.892-.234-1.2-.574-.309.34-.713.574-1.2.574-.486 0-.892-.233-1.2-.574-.31.34-.714.574-1.2.574a.5.5 0 0 1 0-1c.212 0 .52-.18.74-.696l.034-.067a.5.5 0 0 1 .886.067c.221.516.528.696.74.696.213 0 .52-.18.74-.696l.035-.067a.5.5 0 0 1 .885.067c.22.516.527.696.74.696s.519-.18.74-.696M12 5.5a.5.5 0 0 0 .5.5h2.293L12 3.207z"/></svg>
    <span>Sales · US2 · round 2 review</span>
  </div>
  <div class="elicit-body">

    <div style="font-size:12px; font-weight:500; text-transform:uppercase; letter-spacing:0.04em; color:var(--text-muted); margin:4px 2px 0;">Acceptance criteria · 2 criteria</div>

    <div class="elicit-group" style="padding:12px 14px; border:0.5px solid var(--border); border-radius:12px;">
      <label class="elicit-question">
        <span style="font-size:12px; padding:2px 8px; border-radius:var(--radius); background:var(--bg-success); color:var(--text-success);">Approved</span>
        <span style="font-family:var(--font-mono); font-size:13px;">A01 · AC2.1</span>
      </label>
      <div style="font-size:13px; color:var(--text-secondary); line-height:1.6; margin:4px 0 8px;">
        The order summary card is 360px wide on desktop and full width on phone.<br>
        <span>Evidence: .order-summary sets 360px, with a phone media query setting 100%. Test in the browser: OrderList at phone width.</span><br>
        <span style="color:var(--text-muted);">Unchanged</span>
      </div>
      <div class="elicit-pills" data-name="A01" data-multi="false">
        <button type="button" class="elicit-pill" data-value="Yes">Yes</button>
        <button type="button" class="elicit-pill" data-value="No" data-other>No</button>
      </div>
      <input type="text" class="elicit-other" data-for="A01" placeholder="Why is this a no?" hidden>
    </div>

    <div class="elicit-group" style="padding:12px 14px; border:0.5px solid var(--border-warning); border-radius:12px; background:var(--bg-warning);">
      <label class="elicit-question">
        <span style="font-size:12px; padding:2px 8px; border-radius:var(--radius); background:var(--bg-success); color:var(--text-success);">Approved</span>
        <span style="font-family:var(--font-mono); font-size:13px;">A02 · AC2.2</span>
      </label>
      <div style="font-size:13px; color:var(--text-warning); line-height:1.6; margin:4px 0 8px;">
        Fixed in round 2. Your reason: "total must exclude cancelled orders". Done: GetMonthTotal aggregate filters out OrderStatus Cancelled. Tag changed Rejected → Approved.<br>
        <span style="color:var(--text-secondary);">The total sums Order.Amount of the customer's orders in the month. Evidence: GetMonthTotal › GetOrders aggregate, filters on CustomerId and OrderDate in month.</span>
      </div>
      <div class="elicit-pills" data-name="A02" data-multi="false">
        <button type="button" class="elicit-pill" data-value="Yes">Yes</button>
        <button type="button" class="elicit-pill" data-value="No" data-other>No</button>
      </div>
      <input type="text" class="elicit-other" data-for="A02" placeholder="Why is this a no?" hidden>
    </div>

    <div style="font-size:12px; font-weight:500; text-transform:uppercase; letter-spacing:0.04em; color:var(--text-muted); margin:12px 2px 0;">Changes · 1 element</div>

    <div class="elicit-group" style="padding:12px 14px; border:0.5px solid var(--border); border-radius:12px;">
      <label class="elicit-question">
        <span style="font-size:12px; padding:2px 8px; border-radius:var(--radius); background:var(--bg-success); color:var(--text-success);">Created</span>
        <span style="font-family:var(--font-mono); font-size:13px;">C01 · Data › Database › Order</span>
      </label>
      <div style="font-size:13px; color:var(--text-secondary); line-height:1.6; margin:4px 0 8px;">
        Attributes: Id (LongInteger, auto), CustomerId (Customer Id, mandatory, Delete), OrderDate (Date, mandatory), OrderStatusId (OrderStatus Id, mandatory), Amount (Currency, mandatory), Notes (Text 500), CreatedOn, UpdatedOn (DateTime, mandatory). Index: (CustomerId, OrderDate).<br>
        <span style="color:var(--text-muted);">Covers: P02, A02 · Unchanged</span>
      </div>
      <div class="elicit-pills" data-name="C01" data-multi="false">
        <button type="button" class="elicit-pill" data-value="Yes">Yes</button>
        <button type="button" class="elicit-pill" data-value="No" data-other>No</button>
      </div>
      <input type="text" class="elicit-other" data-for="C01" placeholder="Why is this a no?" hidden>
    </div>

  </div>
  <div class="elicit-footer">
    <button type="button" class="elicit-skip">Skip</button>
    <button type="button" data-bulk="Yes" style="background:transparent; border:0.5px solid var(--border); border-radius:var(--radius); padding:6px 12px; font-size:13px; color:var(--text-primary); cursor:pointer;">Yes to all</button>
    <button type="button" data-bulk="No" style="background:transparent; border:0.5px solid var(--border); border-radius:var(--radius); padding:6px 12px; font-size:13px; color:var(--text-primary); cursor:pointer;">No to all</button>
    <button type="button" class="elicit-submit">Submit review</button>
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
