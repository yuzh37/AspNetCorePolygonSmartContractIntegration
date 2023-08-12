// MyNFT.sol
// SPDX-License-Identifier: MIT
pragma solidity ^0.8.8;

import "@openzeppelin/contracts/token/ERC1155/ERC1155.sol";
import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "hardhat/console.sol";

contract MyNFT is ERC1155, Ownable{
    using Counters for Counters.Counter;
    Counters.Counter private itemId;
    address public buyAndSellAddress;
    address public auctionAddress;
    mapping(uint256 => ITEM) private IdToItem;

    struct ITEM {
        uint256 id;
        address owner;
        bool onSell;
    }

    event FreeItemMinted(
        uint256 indexed id,
        address indexed minter
    );

    constructor() ERC1155("") {
    }

    function MintNFT() external {
        itemId.increment();
        uint256 currentId = itemId.current();
        IdToItem[currentId].id = currentId;
        IdToItem[currentId].owner = msg.sender;
        IdToItem[currentId].onSell = false;
        _mint(msg.sender, currentId, 1, "");
        console.log("sender = '%s'",msg.sender);
        emit FreeItemMinted(currentId, msg.sender);
    }
    function getNFTDetail(uint256 _itemId)
        external
        view
        returns (ITEM memory)
    {
        return IdToItem[_itemId];
    }
    function getUserNFTs(address _user)
        external
        view
        returns (ITEM[] memory)
    {
        uint256 totalItems = itemId.current();
        uint256 userItemsCounter = 0;
        uint256 currentIndex = 0;
        // get length
        for (uint256 i = 1; i <= totalItems; i++) {
            if (IdToItem[i].owner == _user) {
                userItemsCounter += 1;
            }
        }
        ITEM[] memory items = new ITEM[](userItemsCounter);

        for (uint256 i = 1; i <= totalItems; i++) {
            if (IdToItem[i].owner == _user) {
                ITEM storage currentItem = IdToItem[i];
                items[currentIndex] = currentItem;
                currentIndex += 1;
            }
        }

        return items;
    }
    function totalNFTsMinted() external view returns (uint256) {
        return itemId.current();
    }
}