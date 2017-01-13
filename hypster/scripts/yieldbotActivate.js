/**
 * Yieldbot.com Intent Tag ACTIVATION
 */
ybotq.push(function () {
  yieldbot.pub('320d');
  yieldbot.defineSlot('LB');
  yieldbot.defineSlot('MR');
  yieldbot.defineSlot('MR_BTF');
  yieldbot.defineSlot('LB_BTF');
  yieldbot.enableAsync();
  yieldbot.go();
});
