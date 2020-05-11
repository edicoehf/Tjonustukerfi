export const getExistingItemWithIds = (item, services, categories) => {
    let idItem = { ...item };
    const s = services[services.findIndex((s) => s.name === item.service)];
    const c = categories[categories.findIndex((c) => c.name === item.category)];
    if (s && c) {
        if (c.id === categories.length) {
            idItem.otherCategory = item.json.otherCategory;
        }
        idItem.service = s.id.toString();
        idItem.category = c.id.toString();
        idItem.filleted = item.json.filleted ? "filleted" : "notFilleted";
        idItem.sliced = item.json.sliced ? "sliced" : "whole";
        idItem.otherCategory = item.json.otherCategory || "";
        idItem.otherService = item.json.otherService || "";
    }
    idItem.categories = categories;
    idItem.services = services;
    return idItem;
};
