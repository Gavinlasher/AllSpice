<template>
  <div
    class="modal fade"
    tabindex="-1"
    role="dialog"
    aria-labelledby="modelTitleId"
    aria-hidden="true"
    id="form-modal"
  >
    <div class="modal-dialog modal-xl" role="document">
      <div class="modal-content bg-transparent text-light">
        <div class="modal-header greenbg">
          <h5 class="modal-title text-light">
            <slot name="title"><!--title slot--></slot>
          </h5>
          <button
            type="button"
            class="btn-close bg-light"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body bg-light">
          <slot name="body"><!--body slot--></slot>
        </div>
        <div class="">
          <slot name="footer"><!--footer slot--></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.greenbg {
  background-color: rgb(95, 160, 95);
}
</style>