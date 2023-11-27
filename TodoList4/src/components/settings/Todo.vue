<template>
    <div>{{ title }}</div>
    <ol>
        <li v-for="(item, index) in items" :key="index">
            {{ item }}
        </li>
    </ol>
</template>

<script>
export default {
    data() {
        return {
            title: '',
            items: ''
        }
    },
    created() {
        this.title = this.$store.getters.getLang.Todo.id
        this.items = this.$store.getters.getLang.Todo.items
    }
}
</script>